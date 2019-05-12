using System;
using Assets.Take_II.Scripts.EnemyManager;
using Assets.Take_II.Scripts.GameManager;
using Assets.Take_II.Scripts.PlayerManager;
using UnityEngine;

namespace Assets.Take_II.Scripts.InputManger {
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

            if (_playerInteractions.Selected != null) {
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
            if (enemy != null && _playerInteractions.Selected == null) {
                EnemyRayCasting (enemy);
                return;
            }

            if (Object.GetComponent<Character> () != null || _playerInteractions.Selected != null) {
                PlayerRaycasting (Object);
                return;
            }
        }

        private void TargetRayCasting (GameObject obj) {

            if (_playerInteractions.IsMoving) return;

            if (!Input.GetMouseButtonDown (0)) return;

            // TODO: Remove ability to target other Characters
            // TODO: Character targeting should depend on Spell
            // ? Possibility: Allow Character targetting for basic attacks
            if (_playerInteractions.Selected != null && _playerInteractions.Target == null) {
                if (_playerInteractions.Selected.gameObject != obj) {
                    if (obj.GetComponent<Tile> () != null) {
                        _playerInteractions.Target = obj;
                    }
                    return;
                }

                var character = obj.GetComponent<Character> ();
                var cameraControl = Camera.main.gameObject.GetComponent<MainCameraController> ();
                if (character != null) {
                    _playerInteractions.Target = obj;
                    cameraControl?.TargetCharacter (character);
                }
                return;
            }

            if (_playerInteractions.Target == obj) {
                // TODO: Move this to End of Action 
                var clearLocation = _playerInteractions.Selected.Location;
                var clearAmount = _playerInteractions.Selected.CurrentMovement;
                _playerInteractions.Act (out bool clearMap);
                if (clearMap) {
                    var clearRange = _playerInteractions.Selected.IsRange;
                    _mapInteractions.ClearReachableArea (clearAmount, clearLocation, clearRange);
                }
                return;
            }

            _playerInteractions.Target = obj;
        }

        private void PlayerRaycasting (GameObject obj) {

            if (_playerInteractions.IsMoving) return;

            if (!Input.GetMouseButtonDown (0)) return;

            if (_playerInteractions.Selected == null) {
                _playerInteractions.Selected = obj.GetComponent<Player> ();
                _mapInteractions.Selected = null;
                if (!_playerInteractions.Selected.TurnFinished && !_playerInteractions.Selected.IsDead) {
                    var drawAmount = _playerInteractions.Selected.CurrentMovement;
                    var drawLocation = _playerInteractions.Selected.Location;
                    var drawRange = _playerInteractions.Selected.IsRange;
                    _mapInteractions.DrawReachableArea (drawAmount, drawLocation, drawRange);
                }
                return;
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