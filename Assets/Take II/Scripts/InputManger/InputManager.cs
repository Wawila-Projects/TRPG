using Assets.Take_II.Scripts.EnemyManager;
using Assets.Take_II.Scripts.GameManager;
using Assets.Take_II.Scripts.HexGrid;
using Assets.Take_II.Scripts.PlayerManager;
using UnityEngine;

namespace Assets.Take_II.Scripts.InputManger
{
    public class InputManager : MonoBehaviour
    {
        public RaycastHit2D Raycast;
        public GameObject Object;

        private MapInteractions _mapInteractions;
        private PlayerInteractions _playerInteractions;
        private EnemyInteractions _enemyInteractions;
        
        [SerializeField]
        public bool escpressed;

        void Awake() {
            _playerInteractions = GetComponent<PlayerInteractions>();
            _mapInteractions = GetComponent<MapInteractions>();
            _enemyInteractions = GetComponent<EnemyInteractions>();
        }

        void Update()
        {
            Raycasting();
            EscapeInput();
        }

        private void EscapeInput() {
            if (!Input.GetKeyDown(KeyCode.Escape) || _playerInteractions?.IsMoving == true) return;

            if (_playerInteractions.Selected != null)
            {
                _mapInteractions.Selected = null;
                _enemyInteractions.Selected = null;
            }

            if (_playerInteractions.Target != null) 
                _playerInteractions.Target = null;
            else
             {
                var clearAmount = _playerInteractions.Selected?.Stats.Movement ?? 0;
                var clearLocation = _playerInteractions.Selected?.Location;
                var clearRange = _playerInteractions.Selected?.IsRange ?? true;
                _mapInteractions.ClearReachableArea(clearAmount, clearLocation, clearRange);
                _playerInteractions.Selected = null;
             }   
        }

        private void Raycasting() {
            Raycast = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (!Raycast) return;

            Object = Raycast.collider.transform.gameObject;
            var enemy = Object.GetComponent<Enemy>();
            if (enemy != null && _playerInteractions.Selected == null)
            {
               EnemyRayCasting(enemy);
                return;
            }
            
            if (Object.GetComponent<Character>() != null || _playerInteractions.Selected != null)
            {
                PlayerRaycasting(Object);
                return;
            }

            var tile = Object.GetComponent<Tile>();
            if (tile != null)
            {
                MapRayCasting(tile);
                return;
            }
        }

        private void PlayerRaycasting(GameObject obj)
        {
            
            if (_playerInteractions.IsMoving) return;

            if (!Input.GetMouseButtonDown(0)) return;

            if (_playerInteractions.Selected == null)
            {
                _playerInteractions.Selected = obj.GetComponent<Player>();
                _mapInteractions.Selected = null;
                var drawAmount = _playerInteractions.Selected.Movement;
                var drawLocation = _playerInteractions.Selected.Location;
                var drawRange = _playerInteractions.Selected.IsRange;
                if (!_playerInteractions.Selected.TurnFinished && !_playerInteractions.Selected.IsDead )
                    _mapInteractions.DrawReachableArea(drawAmount, drawLocation, drawRange);
            }
            else if (_playerInteractions.Selected != null && _playerInteractions.Target == null &&
                     _playerInteractions.Selected.gameObject != obj)
            {
                _playerInteractions.Target = obj;
            }
            else if (_playerInteractions.Target == obj)     
            {
                bool clearMap;
                var clearAmount = _playerInteractions.Selected.Movement;
                var clearLocation = _playerInteractions.Selected.Location;
                var clearRange = _playerInteractions.Selected.IsRange;
                _playerInteractions.Act(out clearMap);
                if(clearMap)
                    _mapInteractions.ClearReachableArea(clearAmount, clearLocation, clearRange);
            }   
            else 
            {
                _playerInteractions.Target = obj;
            }
        }

        public void MapRayCasting(Tile tile)
        {
            if (_playerInteractions.IsMoving) return;

            if (Input.GetMouseButtonDown(0))
            {
                _mapInteractions.Selected = tile;
            }
        }

        public void EnemyRayCasting(Enemy enemy)
        {
            if (_playerInteractions.IsMoving) return;

            if (Input.GetMouseButtonDown(0))
            {
                _enemyInteractions.Selected = enemy;
            }
        }
    } 
}