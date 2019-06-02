using System.Linq;
using Assets.CombatSystem;
using Assets.EnemySystem;
using Assets.PlayerSystem;
using Assets.SpellCastingSystem;
using Assets.Spells;
using UnityEngine;
using Character = Assets.ChracterSystem.Character;

//TODO: Selecting tile out of range bug
//TODO: Undos selection

namespace Assets.InputSystem {
    public class PlayerInteractions : MonoBehaviour {
        public Player Selected;
        public GameObject Target;
        [SerializeField]
        private Character _target;
        [SerializeField]
        private Player _selected;

        public bool IsMoving;
        public bool IsInCombat;
        public bool IsHealing;

        public bool isTargeting => spellTargeting.isTargeting;

        public SpellTargeting spellTargeting;

        void Update () {
            if (isTargeting) return;
            if (SpellCastingDetection ()) return;

            if (IsMoving) {
                Move ();
                return;
            }

            if (_target != null) {
                OnPlayerAction ();
            } else if (IsInCombat) {
                CombatManager.Manager.BasicAttack (Selected, Target.GetComponent<Character> ());
                IsInCombat = false;
                ClearSelected ();
            } else if (IsHealing) {
                IsHealing = false;
                ClearSelected ();
            }
        }

        public bool Act () {
            var isRange = Selected.IsRange;
            if (Selected.TurnFinished) {
                ClearSelected ();
                return true;
            }

            var clearMap = false;
            if (!ActOnTile (ref clearMap)) {
                ActOnPlayer (ref clearMap);
            }
            return clearMap;
        }

        private bool ActOnPlayer (ref bool clearMap) {
            var target = Target.GetComponent<Character> ();

            if (target == null) return false;

            _target = target.ClonePlayer ();

            _selected = Selected.ClonePlayer ();
            var tileInRange = Selected.MoveToRange (target);

            if (tileInRange == null) {
                clearMap = OnPlayerAction ();
                return true;
            }

            Target = tileInRange.gameObject;
            IsMoving = true;
            clearMap = true;
            return true;
        }

        private bool OnPlayerAction () {
            if (_target == null || !_selected.IsInRange (_target))
                return false;

            if (_target is Enemy) {
                IsInCombat = true;
            } else if (_target is Player) {
                IsHealing = true;
            }

            Target = _target.gameObject;
            if (Selected == null)
                Selected = _selected;
            _target = null;
            _selected = null;
            return true;
        }

        private bool ActOnTile (ref bool clearMap, bool moveTowardsUnreachable = false) {
            var tile = Target.GetComponent<Tile> ();
            if (tile == null) return false;

            if (tile.Occupant != null) {
                Target = tile.Occupant.gameObject;
                clearMap = false;
                return true;
            }

            if (!IsReachable ()) {
                if (moveTowardsUnreachable) {
                    var path = AStar.FindPath (Selected.Location, tile);
                    if (path.Count == 0) return false;
                    Target = path.ElementAt (Selected.CurrentMovement).gameObject;
                    IsMoving = true;
                    Selected.CurrentMovement = 0;
                } else {
                    ClearSelected ();
                }

                clearMap = true;
                return true;
            }

            IsMoving = true;
            Selected.CurrentMovement -= Selected.Location.GetDistance (tile);
            clearMap = true;
            return true;

            bool IsReachable () {
                var distance = Selected.Location.GetDistance (tile);
                return distance <= Selected.CurrentMovement;
            }
        }

        private bool SpellCastingDetection () {
            if (Selected == null) return false;
            var spellbook = Selected.Persona.SpellBook;
            for (int i = 0; i < 10; ++i) {
                if (Input.GetKeyDown ($"{i}")) {
                    var spell = spellbook.Spells.ElementAtOrDefault (i);
                    spellTargeting.SelectSpell (spell);
                    return spell != null;
                }
            }
            return false;
        }

        private void Move () {
            var tile = Target.GetComponent<Tile> ();
            if (tile == null)
                return;

            var dest = Target.transform.position;

            if (Selected.transform.position == dest) {
                Selected.Location.Occupant = null;
                tile.Occupant = Selected;
                Selected.Location = tile;
                IsMoving = false;

                var enemy = CheckForAllOutAttack ();
                if (enemy != null) {
                    IsInCombat = false;
                    enemy.IsSurrounded = true;
                    Selected.TurnFinished = true;
                    CombatManager.Manager.AllOutAttack (enemy);
                    _selected = null;
                    _target = null;
                }

                ClearSelected ();
                return;
            }

            var destination = Vector3.MoveTowards (Selected.transform.position, dest, 5 * Time.deltaTime);
            Selected.transform.position = destination;
        }

        private Enemy CheckForAllOutAttack (bool needsSixCount = false) {
            if (IsInCombat || _target is Character) return null;

            foreach (var neighhbor in Selected.Location.Neighbors) {
                if (neighhbor.Occupant == null) continue;
                var enemy = neighhbor.Occupant as Enemy;
                if (enemy == null) continue;
                if (enemy.IsSurrounded) continue;

                if (needsSixCount || enemy.Location.Neighbors.Count == 6 &&
                    enemy.Location.Neighbors.TrueForAll (tile => tile.Occupant is Player))
                    return enemy;
            }

            return null;
        }

        private void ClearSelected () {
            Selected = null;
            Target = null;
        }
    }
}