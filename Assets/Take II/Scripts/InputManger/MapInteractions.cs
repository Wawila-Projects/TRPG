using UnityEngine;

namespace Assets.Take_II.Scripts.InputManger {
    public class MapInteractions : MonoBehaviour {
        public Tile Selected;

        public void DrawReachableArea (int total, Tile selected, bool isRange = false) {
            ColorReachableArea (total, selected, Color.green, isRange);
        }

        public void ClearReachableArea (int total, Tile selected, bool isRange = false) {
            ColorReachableArea (total, selected, Color.white, isRange);
        }
        public void ColorReachableArea (int total, Tile selected, Color color, bool isRange) {
            if (selected == null)
                return;

            var tiles = selected.GetTilesInsideRange (total);
            foreach (var tile in tiles) {
                var renderer = tile.GetComponent<Renderer> ();
                renderer.material.color = color;
            }

            var attackRange = isRange ? 2 : 1;
            tiles = selected.GetTilesAtDistance (total + attackRange);
            foreach (var tile in tiles) {
                var renderer = tile.GetComponent<Renderer> ();
                renderer.material.color = color == Color.white ? Color.white : Color.red;
            }
        }
    }
}