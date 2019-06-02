using System;
using Assets.EnemySystem;
using Assets.GameSystem;
using Assets.ChracterSystem;
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

            if (PlayerInteractions.Selected != null) {
                MapInteractions.ClearReachableArea (PlayerInteractions.Selected);
            }
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

            var player = Object.GetComponent<Player> () ;
            if (player != null && 
                (selected?.TurnFinished ?? true)) {
                PlayerRaycasting (player);
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
                if (PlayerInteractions.Act ()) {
                    MapInteractions.ClearReachableArea (PlayerInteractions.Selected);
                }
                return true;
            }

            PlayerInteractions.Target = target;
            return true;
        }

        private void PlayerRaycasting (Player obj) {

            if (PlayerInteractions.IsMoving) return;

            if (!Input.GetMouseButtonDown (0)) return;

            PlayerInteractions.Selected = obj;
            MapInteractions.Selected = null;
            if (!PlayerInteractions.Selected.TurnFinished && !PlayerInteractions.Selected.IsDead) {
                MapInteractions.DrawReachableArea (PlayerInteractions.Selected);
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