using UnityEngine;

namespace Assets.Take_II.Scripts.InputManger
{
    public class MapInteractions : MonoBehaviour
    {
        public Tile Selected;

        public void DrawReachableArea(int total, Tile selected, bool isRange = false) {
            ColorReachableArea(total, selected, Color.red, isRange);
        }

        public void ClearReachableArea(int total, Tile selected, bool isRange = false) {
            ColorReachableArea(total, selected, Color.white, isRange);
        }
        public void ColorReachableArea(int total, Tile selected, Color color, bool isRange)
        {
            if (selected == null) 
                return;

            var tiles = selected.GetTilesInsideRange(total);
            foreach (var tile in tiles)
            {
                if (isRange && selected.Neighbors.Contains(tile)) {
                    continue;
                }

                var sprite = tile.GetComponentInChildren<SpriteRenderer>();
                sprite.color = color;
            }
        }
    }
}