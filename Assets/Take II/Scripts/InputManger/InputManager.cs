using System.Collections.Generic;
using Assets.Take_II.Scripts.HexGrid;
using UnityEngine;

namespace Assets.Take_II.Scripts.InputManger
{
    public class InputManager : MonoBehaviour
    {
        public Tile Current;
        public Tile Target;
        private readonly AStar _pathfinding = new AStar();
        public List<Tile> List;
        public int Total;

        void Update()
        {
            MapRayCasting();
        }

        private void PlayerRaycasting()
        {
            
        }

        private void MapRayCasting()
        {
            var raycast = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, 9);

            if (!raycast) return;

            var obj = raycast.collider.transform.gameObject;
            
            var tile = obj.GetComponent<Tile>();
            if (tile == null) return;

            if (Input.GetMouseButtonDown(0))
            {
                if (Current == null)
                {
                    Current = tile;
                    DrawReachableArea(Total, Current);
                }
                else
                    Target = tile;

               // if (!DrawPath()) return;
            }


            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //ClearPath();
                ClearReachableArea(Total, Current);

                Total = 2;

                if (Target == null)
                    Current = null;
                else
                    Target = null;
            }
                
        }

        private static void DrawReachableArea(int total, Tile selected)
        {
            if(total < 0 || selected == null) return;

            total -= selected.Cost;

            SpriteRenderer sprite;

            foreach (var neighbor in selected.Neighbors)
            {
                sprite = neighbor.GetComponentInChildren<SpriteRenderer>();

                if (sprite.color != Color.green)
                    sprite.color = Color.red;

                DrawReachableArea(total, neighbor.GetComponent<Tile>());
            }

            sprite = selected.GetComponentInChildren<SpriteRenderer>();
            sprite.color = Color.green;
        }

        private static void ClearReachableArea(int total, Tile selected)
        {
            if (total < 0 || selected == null) return;

            total -= selected.Cost;

            SpriteRenderer sprite;

            foreach (var neighbor in selected.Neighbors)
            {
                sprite = neighbor.GetComponentInChildren<SpriteRenderer>();
                sprite.color = Color.white;
                ClearReachableArea(total, neighbor.GetComponent<Tile>());
            }

            sprite = selected.GetComponentInChildren<SpriteRenderer>();
            sprite.color = Color.white;
        }



        private bool DrawPath()
        {
            if (Current == null || Target == null) return false;

            List = _pathfinding.FindPath(Current, Target);

            foreach (var tile in List)
            {
                if (Total < tile.Cost)
                    return false;

                SpriteRenderer s;

                /*
                foreach (var neighbor in tile.Neighbors)
                {
                    s = neighbor.GetComponentInChildren<SpriteRenderer>();

                    if (s.color == Color.green) continue;
                    s.color = Color.red;
                }
                */

                s = tile.GetComponentInChildren<SpriteRenderer>();
                s.color = Color.green;

                Total -= tile.Cost;
            }

            return true;
        }

        private void ClearPath()
        {
            if (List.Count == 0)
                return;

            foreach (var tile in List)
            {
                SpriteRenderer s;
                foreach (var neighbor in tile.Neighbors)
                {
                    s = neighbor.GetComponentInChildren<SpriteRenderer>();
                    s.color = Color.white;
                }

                s = tile.GetComponentInChildren<SpriteRenderer>();
                s.color = Color.white;
            }
            List.Clear();
        }
    }
}