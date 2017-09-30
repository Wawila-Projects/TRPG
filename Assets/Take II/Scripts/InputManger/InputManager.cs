using System;
using System.Collections.Generic;
using Assets.Take_II.Scripts.HexGrid;
using UnityEngine;

namespace Assets.Take_II.Scripts.InputManger
{
    public class InputManager : MonoBehaviour
    {
        public GameObject Current;
        public GameObject Target;
        public RaycastHit2D HitInfo;
        private AStar a = new AStar();
        public List<Tile> List;
        public int Total;
        private bool depleted;

        void Update()
        {

            HitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0f);

            if (HitInfo)
            {
                var h = HitInfo.collider.transform.gameObject;
                /*
                if(Current.GetComponent<Collider2D>().transform != null)
                    Debug.Log(Current.GetComponent<Collider2D>().transform.name);
                    */

                var t = h.GetComponent<Tile>();
                if (t != null)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if(Current == null)
                            Current = h;
                        else if (Target == null)
                            Target = h;

                        if (Current == null || Target == null) return;

                        List = a.FindPath(Current.GetComponent<Tile>(), Target.GetComponent<Tile>());

                        foreach (var tile in List)
                        {
                            SpriteRenderer s;
                            foreach (var neighbor in tile.Neighbors)
                            {
                                s = neighbor.GetComponentInChildren<SpriteRenderer>();

                                if (s.color == Color.green || s.color == Color.blue) continue;
                                s.color = Color.red;

                                if (depleted)
                                    s.color = Color.yellow;
                            }

                            s = tile.GetComponentInChildren<SpriteRenderer>();
                            s.color = depleted ? Color.blue : Color.green;

                            if (Total < tile.Cost && !depleted)
                                depleted = true;

                            Total -= tile.Cost;

                        }
                    }

                    if (Input.GetKeyDown(KeyCode.Escape))
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

                            Current = null;
                            Target = null;
                        }

                        List.Clear();
                    }
                }
                else if (Current.GetComponent<Player>() != null)
                {

                }
            }
            else
            {
                Debug.Log("1");

            }
        }

        void OnDrawGizmos()
        {
            Gizmos.DrawRay(Input.mousePosition, Vector3.forward);
        }
    }
}