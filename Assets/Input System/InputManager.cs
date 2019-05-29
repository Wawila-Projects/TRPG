using System;
using Assets.EnemySystem;
using Assets.GameSystem;
using Assets.PlayerSystem;
using Assets.SpellCastingSystem;
using UnityEngine;

namespace Assets.InputSystem {
    public class InputManager : MonoBehaviour {
        public RaycastHit Raycast;
        public GameObject Object;

        public MapInteractions MapInteractions;
        public PlayerInteractions PlayerInteractions;
        public EnemyInteractions EnemyInteractions;
        public SpellTargeting SpellTargeter;

        void Awake () {
            PlayerInteractions = GetComponent<PlayerInteractions> ();
            MapInteractions = GetComponent<MapInteractions> ();
            EnemyInteractions = GetComponent<EnemyInteractions> ();
            SpellTargeter = GetComponent<SpellTargeting> ();
        }

        void Update () {
            if (SpellTargeter.isTargeting) return;

            Raycasting ();
            EscapeInput ();
        }

        private void EscapeInput () {
            if (!Input.GetKeyDown (KeyCode.Escape) || PlayerInteractions?.IsMoving == true) return;

            var cameraControl = Camera.main.gameObject.GetComponent<MainCameraController> ();
            if (cameraControl != null) {
                cameraControl.ToTarget = null;
            }

            if (PlayerInteractions.Selected != null) {
                MapInteractions.Selected = null;
                EnemyInteractions.Selected = null;
            }

            if (PlayerInteractions.Target != null) {
                PlayerInteractions.Target = null;
                return;
            }

            var clearAmount = PlayerInteractions.Selected?.Movement ?? 0;
            var clearLocation = PlayerInteractions.Selected?.Location;
            var clearRange = PlayerInteractions.Selected?.IsRange ?? true;
            MapInteractions.ClearReachableArea (clearAmount, clearLocation, clearRange);
            PlayerInteractions.Selected = null;
        }

        private void Raycasting () {
            var ray = Camera.main.ScreenPointToRay (Input.mousePosition);

            if (!Physics.Raycast (ray, out Raycast)) {
                Object = null;
                return;
            }

            Object = Raycast.collider.transform.gameObject;
            var selected = PlayerInteractions.Selected;

            if (!selected?.TurnFinished ?? false) {
                TargetRayCasting (Object);
                return;
            }

            var tile = Object.GetComponent<Tile> ();
            if (tile != null) {
                if (!tile.IsOccupied) {
                    MapRayCasting (tile);
                    return;
                }
                Object = tile.Occupant.gameObject;
            }

            var enemy = Object.GetComponent<Enemy> ();
            if (enemy != null && selected == null) {
                EnemyRayCasting (enemy);
                return;
            }

            if (Object.GetComponent<Character> () != null || 
                (selected?.TurnFinished ?? false)) {
                PlayerRaycasting (Object);
                return;
            }
        }

        private bool TargetRayCasting (GameObject obj) {

            if (PlayerInteractions.IsMoving) return false;
            if (PlayerInteractions.Selected is null) return false;
            if (!Input.GetMouseButtonDown (0)) return false;

            // Zoom on when reselecting self

            var target = obj.GetComponent<Tile> ()?.Occupant?.gameObject ?? obj;

            if (target == PlayerInteractions.Selected.gameObject) {
                var cameraControl = Camera.main.gameObject.GetComponent<MainCameraController> ();
                cameraControl?.TargetCharacter (PlayerInteractions.Selected);
            }
            // No Target
            if (PlayerInteractions.Target == null) {
                PlayerInteractions.Target = target;
                return true;
            }

            // Confirm Action
            if (PlayerInteractions.Target == target) {
                // TODO: Move this to End of Action 
                var clearLocation = PlayerInteractions.Selected.Location;
                var clearAmount = PlayerInteractions.Selected.CurrentMovement;
                var (clearMap, isRange) = PlayerInteractions.Act ();
                if (clearMap) {
                    MapInteractions.ClearReachableArea (clearAmount, clearLocation, isRange);
                }
                return true;
            }

            PlayerInteractions.Target = target;
            return true;
        }

        private void PlayerRaycasting (GameObject obj) {

            if (PlayerInteractions.IsMoving) return;

            if (!Input.GetMouseButtonDown (0)) return;

            PlayerInteractions.Selected = obj.GetComponent<Player> ();
            MapInteractions.Selected = null;
            if (!PlayerInteractions.Selected.TurnFinished && !PlayerInteractions.Selected.IsDead) {
                var drawAmount = PlayerInteractions.Selected.CurrentMovement;
                var drawLocation = PlayerInteractions.Selected.Location;
                var drawRange = PlayerInteractions.Selected.IsRange;
                MapInteractions.DrawReachableArea (drawAmount, drawLocation, drawRange);
            }
        }

        public void MapRayCasting (Tile tile) {
            if (PlayerInteractions.IsMoving) return;

            if (Input.GetMouseButtonDown (0)) {
                MapInteractions.Selected = tile;
            }
        }

        public void EnemyRayCasting (Enemy enemy) {
            if (PlayerInteractions.IsMoving) return;

            if (Input.GetMouseButtonDown (0)) {
                EnemyInteractions.Selected = enemy;
            }
        }
    }
}