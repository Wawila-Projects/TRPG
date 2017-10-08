using Assets.Take_II.Scripts.HexGrid;
using UnityEngine;

namespace Assets.Take_II.Scripts.InputManger
{
    public class MapInteractions : MonoBehaviour
    {
        public Tile Selected;

        //void Update()
        //{
        //    if(Selected == null)
        //        return;

        //    DrawTileInfo();
        //}

        public void DrawReachableArea(int total, Tile selected)
        {
            if (total < 0 || selected == null) return;

            total -= selected.Cost;

            SpriteRenderer sprite;

            foreach (var neighbor in selected.Neighbors)
            {
                sprite = neighbor.GetComponentInChildren<SpriteRenderer>();

                if (sprite.color != Color.cyan)
                    sprite.color = Color.red;

                DrawReachableArea(total, neighbor.GetComponent<Tile>());
            }

            sprite = selected.GetComponentInChildren<SpriteRenderer>();
            sprite.color = Color.cyan;
        }

        public void ClearReachableArea(int total, Tile selected)
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
    }
}