using System.Linq;
using Assets.Take_II.Scripts.Combat;
using Assets.Take_II.Scripts.EnemyManager;
using Assets.Take_II.Scripts.PlayerManager;
using UnityEngine;
using Character = Assets.Take_II.Scripts.GameManager.Character;

namespace Assets.Take_II.Scripts.InputManger {
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

        void Update () {
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

        public void Act (out bool clearMap) {
            if (Selected.TurnFinished) {
                ClearSelected ();
                clearMap = true;
                return;
            }

            clearMap = false;
            if (ActOnTile (ref clearMap)) return;
            ActOnPlayer (ref clearMap);
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

            if (!IsReachable()) {
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
            Selected.CurrentMovement -= Mathf.RoundToInt (Selected.Location.GetDistance (tile));
            clearMap = true;
            return true;

            bool IsReachable () {
                var distance = Selected.Location.GetDistance (tile);
                return distance <= Selected.CurrentMovement;
            }
        }

        public void Move () {
            var tile = Target.GetComponent<Tile> ();
            if (tile == null)
                return;

            var dest = new Vector3 {
                x = tile.WorldX,
                y = tile.WorldY,
                z = Selected.transform.position.z
            };

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

            var destination = Vector3.MoveTowards (Selected.transform.position, dest, 3 * Time.deltaTime);
            Selected.transform.position = destination;
        }

        private Enemy CheckForAllOutAttack () {
            if (IsInCombat || _target is Character) return null;

            foreach (var neighhbor in Selected.Location.Neighbors) {
                if (neighhbor.Occupant == null) continue;
                var enemy = neighhbor.Occupant as Enemy;
                if (enemy == null) continue;
                if (enemy.IsSurrounded) continue;

                if (enemy.Location.Neighbors.TrueForAll (tile => tile.Occupant is Player))
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