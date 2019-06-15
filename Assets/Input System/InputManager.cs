using System;
using Assets.EnemySystem;
using Assets.GameSystem;
using Assets.CharacterSystem;
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
            GameController.Manager?.UIManager.EnemyStatus.Hide();
            if (SpellTargeter.isTargeting) return;
            if (TurnManager.Manager.EnemyPhase) return;
            Raycasting ();
            EscapeInput ();
        }

        private void EscapeInput () {
            if (!Input.GetKeyDown (KeyCode.Escape) && 
                !Input.GetMouseButtonDown(1)) return;

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

            var selected = PlayerInteractions.Selected;
            if (PlayerInteractions.Selected != null) {
                MapInteractions.ClearReachableArea (selected);
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

            if (PlayerInteractions.Selected is null) return false;
            
            var target = obj.GetComponent<Tile> ()?.Occupant?.gameObject ?? obj;

            var enemy = target.GetComponent<Enemy> ();
            if (enemy != null) {
                GameController.Manager.UIManager.EnemyStatus.Show(enemy);
            }
            
            if (!Input.GetMouseButtonDown (0)) return false;

            // Zoom on when reselecting self
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
                var selected = PlayerInteractions.Selected;
                if (PlayerInteractions.Act ()) {
                    MapInteractions.ClearReachableArea (selected);
                }
                return true;
            }

            PlayerInteractions.Target = target;
            return true;
        }

        private void PlayerRaycasting (Player obj) {


            if (!Input.GetMouseButtonDown (0)) return;

            PlayerInteractions.Selected = obj;
            MapInteractions.Selected = null;
            if (!PlayerInteractions.Selected.TurnFinished && !PlayerInteractions.Selected.IsDead) {
                MapInteractions.DrawReachableArea (PlayerInteractions.Selected);
            }
        }

        public void MapRayCasting (Tile tile) {
            if (Input.GetMouseButtonDown (0)) {
                MapInteractions.Selected = tile;
            }
        }

        public void EnemyRayCasting (Enemy enemy) {
            GameController.Manager.UIManager.EnemyStatus.Show(enemy);
            
            if (Input.GetMouseButtonDown (0)) {
                EnemyInteractions.Selected = enemy;
            }
        }
    }
}