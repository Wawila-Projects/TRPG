using Assets.Take_II.Scripts.HexGrid;
using Assets.Take_II.Scripts.PlayerManager;
using UnityEngine;

namespace Assets.Take_II.Scripts.InputManger
{
    public class InputManager : MonoBehaviour
    {
        private MapInteractions MapInteractions;
        private PlayerInteractions PlayerInteractions; 
        public RaycastHit2D raycast;
        public GameObject _object;

        void Awake() {
            PlayerInteractions = GetComponent<PlayerInteractions>();
            MapInteractions = GetComponent<MapInteractions>();
        }

        void Update()
        {
            raycast = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (!raycast) return;

            _object = raycast.collider.transform.gameObject;

            var tile = _object.GetComponent<Tile>();

            if (_object.GetComponent<Player>() != null || PlayerInteractions.Selected != null)
            {
                PlayerRaycasting(_object);
            }
            else if(tile != null)
            {
                MapRayCasting(tile);
            }

            if (!Input.GetKeyDown(KeyCode.Escape) || PlayerInteractions.IsMoving) return;

            if (PlayerInteractions.Selected != null)
            {
                var clearAmount = PlayerInteractions.Selected.Stats.Movement;
                var clearLocation = PlayerInteractions.Selected.Location;
                var clearRange = PlayerInteractions.Selected.IsRange;
                MapInteractions.ClearReachableArea(clearAmount, clearLocation, clearRange);

                MapInteractions.Selected = null;
            }

            if (PlayerInteractions.Target != null) 
                PlayerInteractions.Target = null;
            else
                PlayerInteractions.Selected = null;
        }

        private void PlayerRaycasting(GameObject obj)
        {
            if (PlayerInteractions.IsMoving) return;

            if (!Input.GetMouseButtonDown(0)) return;

            if (PlayerInteractions.Selected == null)
            {
                PlayerInteractions.Selected = obj.GetComponent<Player>();
                MapInteractions.Selected = null;
                var drawAmount = PlayerInteractions.Selected.Stats.Movement;
                var drawLocation = PlayerInteractions.Selected.Location;
                var drawRange = PlayerInteractions.Selected.IsRange;
                MapInteractions.DrawReachableArea(drawAmount, drawLocation, drawRange);
            }
            else if (PlayerInteractions.Selected != null && PlayerInteractions.Target == null &&
                     PlayerInteractions.Selected.gameObject != obj)
            {
                PlayerInteractions.Target = obj;
            }
            else if (PlayerInteractions.Target == obj)     
            {
                bool clearMap;
                var clearAmount = PlayerInteractions.Selected.Stats.Movement;
                var clearLocation = PlayerInteractions.Selected.Location;
                var clearRange = PlayerInteractions.Selected.IsRange;
                PlayerInteractions.Act(out clearMap);
                if(clearMap)
                    MapInteractions.ClearReachableArea(clearAmount, clearLocation, clearRange);
            }   
            else 
            {
                PlayerInteractions.Target = obj;
            }
        }

        public void MapRayCasting(Tile tile)
        {
            if (PlayerInteractions.IsMoving) return;

            if (Input.GetMouseButtonDown(0))
            {
                if (MapInteractions.Selected != null) return;

                MapInteractions.Selected = tile;
            }

        }
    } 
}