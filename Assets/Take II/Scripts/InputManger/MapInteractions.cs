using Assets.Take_II.Scripts.HexGrid;
using UnityEngine;

namespace Assets.Take_II.Scripts.InputManger
{
    public class MapInteractions : MonoBehaviour
    {
        public Tile Selected;

        public void DrawReachableArea(int total, Tile selected, bool isRange = false)
        {
            if (selected == null) 
                return;

            if (total < 0) {
                if (!isRange) return;
                PaintNeighbors(selected, Color.red);
                return;
            }
            
            total -= selected.Cost;

            SpriteRenderer sprite;

            foreach (var neighbor in selected.Neighbors)
            {
                sprite = neighbor.GetComponentInChildren<SpriteRenderer>();
                if (sprite.color != Color.cyan)
                    sprite.color = Color.red;
                DrawReachableArea(total, neighbor.GetComponent<Tile>(), isRange);
            }

            sprite = selected.GetComponentInChildren<SpriteRenderer>();
            sprite.color = Color.cyan;
        }

        private void PaintNeighbors(Tile selected, Color color) {
            if (selected == null)
                return;

           foreach (var neighbor in selected.Neighbors)
            {
                var sprite = neighbor.GetComponentInChildren<SpriteRenderer>();
                if (sprite.color != Color.cyan)
                    sprite.color = color;
            }
        }

        public void ClearReachableArea(int total, Tile selected, bool isRange = false)
        {
            if (selected == null)
                return;

            if (total < 0) {
                if (!isRange) return;
                PaintNeighbors(selected, Color.white);
                return;
            }

            total -= selected.Cost;

            SpriteRenderer sprite;

            foreach (var neighbor in selected.Neighbors)
            {
                sprite = neighbor.GetComponentInChildren<SpriteRenderer>();
                sprite.color = Color.white;
                ClearReachableArea(total, neighbor.GetComponent<Tile>(), isRange);
            }

            sprite = selected.GetComponentInChildren<SpriteRenderer>();
            sprite.color = Color.white;
        }
    }
}