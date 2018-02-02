using Assets.Take_II.Scripts.HexGrid;
using Assets.Take_II.Scripts.PlayerManager;
using UnityEngine;

namespace Assets.Take_II.Scripts.InputManger
{
    public class InputManager : MonoBehaviour
    {

        public MapInteractions MapInteractions { get; set; }
        public PlayerInteractions PlayerInteractions { get; set; }


        void Update()
        {
            var raycast = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (!raycast) return;

            var obj = raycast.collider.transform.gameObject;

            var tile = obj.GetComponent<Tile>();
            

            if (obj.GetComponent<Player>() != null || PlayerInteractions.Selected != null)
            {
                PlayerRaycasting(obj);
            }
            else if(tile != null)
            {
                MapRayCasting(tile);
            }

            if (Input.GetKeyDown(KeyCode.Escape) && !PlayerInteractions.IsMoving)
            {
                if (PlayerInteractions.Selected != null)
                {
                    MapInteractions.ClearReachableArea(PlayerInteractions.Selected.Stats.Movement,
                        PlayerInteractions.Selected.Location);

                    MapInteractions.Selected = null;
                }

                if (PlayerInteractions.Target != null)
                {
                    PlayerInteractions.Target = null;

                }
                else
                    PlayerInteractions.Selected = null;

            }
        }

        private void PlayerRaycasting(GameObject obj)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (PlayerInteractions.Selected == null)
                {
                    PlayerInteractions.Selected = obj.GetComponent<Player>();
                    MapInteractions.Selected = null;

                    MapInteractions.DrawReachableArea(PlayerInteractions.Selected.Stats.Movement, PlayerInteractions.Selected.Location);
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

                    PlayerInteractions.Act(out clearMap);
                    if(clearMap)
                        MapInteractions.ClearReachableArea(clearAmount, clearLocation);
                }   
                else 
                {
                    PlayerInteractions.Target = obj;
                }
            }
        }

        public void MapRayCasting(Tile tile)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (MapInteractions.Selected != null) return;

                MapInteractions.Selected = tile;
            }

        }
    } 
}