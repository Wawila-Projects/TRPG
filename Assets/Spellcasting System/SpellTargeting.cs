using System.Collections.Generic;
using System.Linq;
using Assets.CombatSystem;
using Assets.EnemySystem;
using Assets.CharacterSystem;
using Assets.InputSystem;
using Assets.PlayerSystem;
using Assets.Spells;
using Assets.Utils;
using UnityEngine;

namespace Assets.SpellCastingSystem {
    public class SpellTargeting : MonoBehaviour {
        public MapCoordinator Map;
        public Player Caster => GetComponent<PlayerInteractions> ().Selected;
        public Tile SelectedTile;
        public SpellBase Spell;
        public bool isTargeting;
        public List<Tile> SpellTiles;
        public List<Color> originalColors;
        private SpellCasting SpellCaster = new SpellCasting();


        public string SpellSerialized;

        void Update () {
            if (!isTargeting || Spell == null ||
                Caster == null || Caster.TurnFinished) {
                isTargeting = false;
                return;
            }

            if (EscapeInput ()) {
                return;
            }

            if (CastSpell ()) {
                return;
            }

            var target = Raycasting ();

            var inRange = false;
            if (SelectedTile != target) {
                SelectedTile = target;
                inRange = Caster.IsInRange (SelectedTile);
            }

            if (SelectedTile == null || !inRange) return;

            ClearSelection ();

            if (Spell.IsMultitarget) {
                SpellTiles = new List<Tile> (SelectedTile.Neighbors);
                SpellTiles.Add (SelectedTile);
                SpellTiles.RemoveAll (t => !Caster.IsInRange (t));
            } else {
                SpellTiles = new List<Tile> () {
                    SelectedTile
                };
            }

            SpellTiles.ForEach (t => {
                originalColors.Add (t.GetColor ());
                t.ChangeColor (Color.blue);
            });
        }

        public void SelectSpell (SpellBase spell) {
            if (spell is null)
                return;

            Spell = spell;
            SpellSerialized = spell.ToString ();
            isTargeting = true;
        }

        private bool CastSpell () {
            if (!Input.GetMouseButtonDown (0)) return false;

            var caster = Caster;
            var shouldReturn = caster is null || Spell is null ||
                SpellTiles.IsEmpty () || CombatManager.Manager is null;

            if (shouldReturn) return false;

            var targets = GetTargets (SpellTiles);

            if (targets.IsEmpty ()) return false;

            SpellCaster.CastSpell (Spell, caster, targets);

            ClearSelection ();
            
            GetComponent<PlayerInteractions> ()?.ClearSelected();
            GetComponent<MapInteractions> ()?.ClearReachableArea (caster);
            isTargeting = false;
            return true;
        }

        private bool EscapeInput () {
            if (!Input.GetKey (KeyCode.Escape) &&
                !Input.GetMouseButtonDown(1))
                return false;

            SelectedTile = null;
            Spell = null;
            SpellSerialized = null;

            ClearSelection ();

            isTargeting = false;
            return true;
        }

        private Tile Raycasting () {
            var ray = Camera.main.ScreenPointToRay (Input.mousePosition);

            if (!Physics.Raycast (ray, out var raycast)) {
                return null;
            }

            var Object = raycast.collider.transform.gameObject;

            var tile = Object.GetComponent<Tile> ();
            if (tile != null) {
                return tile;
            }

            var character = Object.GetComponent<Character> ();
            if (character != null) {
                return tile;
            }
            return null;
        }

        private void ClearSelection () {
            SpellTiles.ForEach ((t, i) => {
                t.ChangeColor (originalColors[i]);
            });
            SpellTiles.Clear ();
            originalColors.Clear ();
        }

        private List<Character> GetTargets (List<Tile> tiles) {
            if (Spell is null) return new List<Character> ();

            var targets = new List<Character> ();

            foreach (var tile in tiles) {
                if (!tile.IsOccupied) {
                    continue;
                }

                var target = tile.Occupant;
                if ((Spell is OffensiveSpell || Spell is AilementSpell) 
                    && target is Enemy) {
                    targets.Add (target);
                    continue;
                }

                if (Spell is RecoverySpell && target is Player) {
                    targets.Add (target);
                }
            }

            return targets;
        }

        // public List<Tile> GetFullTiles (Tile origin, bool isRange) {
        //     if (!isRange) return origin.Neighbors;
        //     return origin.GetTilesAtDistance (2);
        // }
        // public List<Tile> GetFrontAndBack (Tile selected, bool isRange) {
        //     var back = Map.TileAt (selected.Hex * -1);
        //     return new List<Tile> {
        //         selected,
        //         back
        //     };
        // }

        // public List<Tile> GetSpaceBetween (Tile selected, bool isRange) {
        //     var tiles = Caster.Location.GetTilesAtDistance (isRange ? 2 : 1);

        //     // var radius = isRange ? 2 : 1;
        //     // var needsReverse = false;

        //     // var index = Caster.Location.GetDirection(selected);
        //     // var startingIndex = (index + 2) % 6;
        //     // var tile = selected;
        //     // for (var i = 0; i < 6; i++) {
        //     //     index = (startingIndex + i) % 6;
        //     //     for (var j = 0; j < radius; j++) {
        //     //         tile = tile.GetNeighborAt(index);
        //     //         if (tile is null) {
        //     //             needsReverse = true;
        //     //             goto reverse;
        //     //         }
        //     //     }
        //     // }

        //     // reverse:
        //     // if (needsReverse) {

        //     // }

        //     tiles.RemoveNull ();
        //     return tiles;
        // }

        // public List<Tile> GetThree (Tile origin, Tile selected, bool isRange) {
        //     if (!isRange && origin != null) {
        //         return origin.Neighbors.Intersect (selected.Neighbors).ToList ();
        //     }

        //     var left = Map.TileAt (selected.Hex.RotateLeft ());
        //     var right = Map.TileAt (selected.Hex.RotateRight ());
        //     var tiles = new List<Tile> () { selected, left, right };
        //     tiles.Add (
        //         left.Neighbors.Intersect (selected.Neighbors).Except (origin.Neighbors).FirstOrDefault ()
        //     );
        //     tiles.Add (
        //         right.Neighbors.Intersect (selected.Neighbors).Except (origin.Neighbors).FirstOrDefault ()
        //     );

        //     tiles.RemoveNull ();
        //     return tiles;
        // }
    }
}