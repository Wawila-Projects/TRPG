using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.CharacterSystem;
using Assets.CombatSystem;
using Assets.GameSystem;
using Assets.PlayerSystem;
using Assets.SpellCastingSystem;
using Assets.Spells;
using Assets.Utils;
using UnityEngine;

namespace Assets.EnemySystem {
    public class Enemy : Character {

        public EnemyAI AI;
        public int BasicAttack;
        public int Accuracy;
        public Player Target;
        public bool IsBoss;
        public bool IsMoving;
        public SpellCasting<Enemy> SpellCasting = new SpellCasting<Enemy>();

        protected override void OnAwake() => AI = new EnemyAI(this, EnemyAI.EnemyTargetCategory.Closest);

        public override void Die () {
            base.Die();

            Location.Occupant = null;
            Location = null;
            gameObject.SetActive(false);

            var enemyStatus = GameController.Manager.UIManager.EnemyStatus;
            if (enemyStatus.Anchor == gameObject) {
                enemyStatus.Hide();
            }
        }

        public void Act () {
            var ai = AI.NextTurnActions ();

            Target = ai.target;

            if (Target == null) {
                TurnFinished = true;
                IsSurrounded = false;
                return;
            }

            // TODO: Add movement actions
            // if (ai.action == EnemyAI.EnemyActions.Stay) {

            // } else 
            // if (ai.action == EnemyAI.EnemyActions.Disengage) {

            // } else {
                
            // } 

            var tileInRange = MoveToRange(ai.target);

            while (tileInRange == null && CurrentMovement > 0) {
                if (IsInRange(Target)) break;

                --CurrentMovement;
                tileInRange = MoveToRange(ai.target);
            }

            if (tileInRange == null && !IsInRange(Target)) {
                IsSurrounded = false;
                TurnFinished = true;
                Debug.Log($"{Name} did NOTHING!");
                return;
            }

            Move (tileInRange, b => {
                if (b || TurnFinished) {
                    TurnFinished = true;
                    IsSurrounded = false;
                    return;
                }

                HandleAttack();

                IsSurrounded = false;
                if (OneMore.isActive) {
                    Act();
                    Debug.Log($"{Name} One More!!");
                } else {
                    TurnFinished = true;
                }
            });


            void HandleAttack ()
            {
                if (ai.action == EnemyAI.EnemyActions.BasicAttack && IsInMeleeRange(Target)) {
                    CombatManager.Manager.BasicAttack(this, Target);
                    return;
                }

                if (ai.action != EnemyAI.EnemyActions.SpellAttack || !IsInRange(Target)) {
                    return;
                }

                var spell = ai.possibleSpells.GetRandomValue();

                var targets = new List<Character> () {
                    Target
                };

                if (spell.IsMultitarget) {
                    targets = Target.Location.Neighbors.Where(
                        w => w.IsOccupied && w.Occupant.GetType() == Target.GetType()
                    )
                    .Select(s => s.Occupant)
                    .ToList();
                }
                
                SpellCasting.CastSpell(spell, this, targets);
            }
        }

        public void Move (Tile destination, Action<bool> completion = null) {
            if (destination == null) {
                completion?.Invoke (false);
                return;
            }

            StartCoroutine (TakeStep ());

            IEnumerator TakeStep () {
                IsMoving = true;
                while (transform.position != destination.transform.position) {
                    transform.position = Vector3.MoveTowards (transform.position,
                        destination.transform.position, 5 * Time.deltaTime);
                    yield return new WaitForEndOfFrame ();
                }

                Location.Occupant = null;
                destination.Occupant = this;
                Location = destination;

                var player = CheckForAllOutAttack ();
                if (player != null) {
                    player.IsSurrounded = true;
                    TurnFinished = true;
                    DeactivateOneMore ();
                    CombatManager.Manager.AllOutAttack (player);
                }

                IsMoving = false;
                completion?.Invoke (player != null);
                yield break;
            }

            Player CheckForAllOutAttack (bool needsSixCount = false) {
                foreach (var neighhbor in Location.Neighbors) {
                    if (neighhbor.Occupant == null) continue;
                    var player = neighhbor.Occupant as Player;
                    if (player == null) continue;
                    if (player.IsSurrounded) continue;

                    if (needsSixCount || player.Location.Neighbors.Count == 6 &&
                        player.Location.Neighbors.TrueForAll (tile => tile.Occupant is Enemy))
                        return player;
                }

                return null;
            }
        }

        public new Enemy ClonePlayer () {
            return GetComponent<Enemy> ().gameObject.GetComponent<Enemy> ();
        }
    }
}