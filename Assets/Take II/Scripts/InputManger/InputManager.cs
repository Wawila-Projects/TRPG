using Assets.Take_II.Scripts.HexGrid;
using Assets.Take_II.Scripts.PlayerManager;
using UnityEngine;

namespace Assets.Take_II.Scripts.InputManger
{
    public class InputManager : MonoBehaviour
    {

        public MapInteractions _mapInteractions;
        public PlayerInteractions _playerInteractions;


        void Update()
        {
            var raycast = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (!raycast) return;

            var obj = raycast.collider.transform.gameObject;

            var tile = obj.GetComponent<Tile>();

            if (obj.GetComponent<Player>() != null || _playerInteractions.Selected != null)
            {
                PlayerRaycasting(obj);
            }
            else if(tile != null)
            {
                MapRayCasting(tile);
            }


            if (Input.GetKeyDown(KeyCode.Escape) && !_playerInteractions.IsMoving)
            {
                if (_playerInteractions.Selected != null)
                {
                    _mapInteractions.ClearReachableArea(_playerInteractions.Selected.Stats.Movement,
                        _playerInteractions.Selected.Location);

                    _mapInteractions.Selected = null;
                }

                if (_playerInteractions.Target != null)
                {
                    _playerInteractions.Target = null;

                }
                else
                    _playerInteractions.Selected = null;

            }
        }

        
        private void PlayerRaycasting(GameObject obj)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (_playerInteractions.Selected == null)
                {
                    _playerInteractions.Selected = obj.GetComponent<Player>();
                    _mapInteractions.Selected = null;

                    _mapInteractions.DrawReachableArea(_playerInteractions.Selected.Stats.Movement, _playerInteractions.Selected.Location);
                }
                else if (_playerInteractions.Selected != null && _playerInteractions.Target == null && _playerInteractions.Selected.gameObject != obj)
                    _playerInteractions.Target = obj;
                // TODO Implement Double Click Correctly
                else if (_playerInteractions.Target == obj)     
                {
                    bool clearMap;
                    var clearAmount = _playerInteractions.Selected.Stats.Movement;
                    var clearLocation = _playerInteractions.Selected.Location;

                    _playerInteractions.Act(out clearMap);
                    if(clearMap)
                        _mapInteractions.ClearReachableArea(clearAmount, clearLocation);
                }   
            }
        }

        public void MapRayCasting(Tile tile)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (_mapInteractions.Selected != null) return;

                _mapInteractions.Selected = tile;
            }

        }
    }
}