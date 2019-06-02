using UnityEngine;
using System.Collections.Generic;
using Assets.SpellCastingSystem;
using Assets.ChracterSystem;
using Assets.PlayerSystem;

namespace Assets.InputSystem {
    public class MapInteractions : MonoBehaviour {
        public Tile Selected;

        public void DrawReachableArea (Character character) {
            DrawReachableArea (character.CurrentMovement, character.Location, character.IsRange);
        }
        
        public void ClearReachableArea (Character character) {
            ClearReachableArea (character.Movement, character.Location, character.IsRange);
        }

        public void DrawReachableArea (int total, Tile selected, bool isRange = false) {
            ColorReachableArea (total, selected, Color.green, isRange);
        }

        public void ClearReachableArea (int total, Tile selected, bool isRange = false) {
            ColorReachableArea (total, selected, Color.white, isRange);
        }
        public void ColorReachableArea (int total, Tile selected, Color color, bool isRange) {
            if (selected == null) return;

            var tiles = selected.GetTilesInsideRange (total);
            tiles.ForEach( t => t.ChangeColor(color));
            
            var attackRange = isRange ? 2 : 1;
            tiles = selected.GetTilesAtDistance (total + attackRange);
            tiles.ForEach( t => t.ChangeColor(color == Color.white ? Color.white : Color.red));
        }
    }
}