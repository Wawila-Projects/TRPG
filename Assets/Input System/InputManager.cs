using System;
using Assets.EnemySystem;
using Assets.GameSystem;
using Assets.PlayerSystem;
using UnityEngine;

namespace Assets.InputSystem {
    public class InputManager : MonoBehaviour {
        public RaycastHit Raycast;
        public GameObject Object;

        public MapInteractions _mapInteractions;
        public PlayerInteractions _playerInteractions;
        public EnemyInteractions _enemyInteractions;

        [SerializeField]
        public bool escpressed;

        void Awake () {
            _playerInteractions = GetComponent<PlayerInteractions> ();
            _mapInteractions = GetComponent<MapInteractions> ();
            _enemyInteractions = GetComponent<EnemyInteractions> ();
        }

        void Update () {
            Raycasting ();
            EscapeInput ();
        }

        private void EscapeInput () {
            if (!Input.GetKeyDown (KeyCode.Escape) || _playerInteractions?.IsMoving == true) return;

            var cameraControl = Camera.main.gameObject.GetComponent<MainCameraController> ();
            if (cameraControl != null) {
                cameraControl.ToTarget = null;
            }

            if (_playerInteractions.Selected != null) {
                _mapInteractions.Selected = null;
                _enemyInteractions.Selected = null;
            }

            if (_playerInteractions.Target != null) {
                _playerInteractions.Target = null;
                return;
            }

            var clearAmount = _playerInteractions.Selected?.Movement ?? 0;
            var clearLocation = _playerInteractions.Selected?.Location;
            var clearRange = _playerInteractions.Selected?.IsRange ?? true;
            _mapInteractions.ClearReachableArea (clearAmount, clearLocation, clearRange);
            _playerInteractions.Selected = null;
        }

        private void Raycasting () {
            var ray = Camera.main.ScreenPointToRay (Input.mousePosition);

            if (!Physics.Raycast (ray, out Raycast)) {
                Object = null;
                return;
            }

            Object = Raycast.collider.transform.gameObject;
            var selected = _playerInteractions.Selected;

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

        private void TargetRayCasting (GameObject obj) {

            if (_playerInteractions.IsMoving) return;
            if (_playerInteractions.Selected is null) return;
            if (!Input.GetMouseButtonDown (0)) return;

            // Zoom on when reselecting self

            var target = obj.GetComponent<Tile> ()?.Occupant?.gameObject ?? obj;

            if (target == _playerInteractions.Selected.gameObject) {
                var cameraControl = Camera.main.gameObject.GetComponent<MainCameraController> ();
                cameraControl?.TargetCharacter (_playerInteractions.Selected);
            }
            // No Target
            if (_playerInteractions.Target == null) {
                _playerInteractions.Target = target;
                return;
            }

            // Confirm Action
            if (_playerInteractions.Target == target) {
                // TODO: Move this to End of Action 
                var clearLocation = _playerInteractions.Selected.Location;
                var clearAmount = _playerInteractions.Selected.CurrentMovement;
                var (clearMap, isRange) = _playerInteractions.Act ();
                if (clearMap) {
                    _mapInteractions.ClearReachableArea (clearAmount, clearLocation, isRange);
                }
                return;
            }

            _playerInteractions.Target = target;
        }

        private void PlayerRaycasting (GameObject obj) {

            if (_playerInteractions.IsMoving) return;

            if (!Input.GetMouseButtonDown (0)) return;

            _playerInteractions.Selected = obj.GetComponent<Player> ();
            _mapInteractions.Selected = null;
            if (!_playerInteractions.Selected.TurnFinished && !_playerInteractions.Selected.IsDead) {
                var drawAmount = _playerInteractions.Selected.CurrentMovement;
                var drawLocation = _playerInteractions.Selected.Location;
                var drawRange = _playerInteractions.Selected.IsRange;
                _mapInteractions.DrawReachableArea (drawAmount, drawLocation, drawRange);
            }
        }

        public void MapRayCasting (Tile tile) {
            if (_playerInteractions.IsMoving) return;

            if (Input.GetMouseButtonDown (0)) {
                _mapInteractions.Selected = tile;
            }
        }

        public void EnemyRayCasting (Enemy enemy) {
            if (_playerInteractions.IsMoving) return;

            if (Input.GetMouseButtonDown (0)) {
                _enemyInteractions.Selected = enemy;
            }
        }
    }
}