using System.Collections.Generic;
using System.Linq;
using Assets.Enums;
using Assets.GameSystem;
using Assets.PlayerSystem;
using Assets.ProbabilitySystem;
using Assets.Spells;
using Assets.Utils;
using UnityEngine;

namespace Assets.EnemySystem {
    [System.Serializable]
    public class EnemyAI {
        public Hivemind Hivemind;
        public Enemy Enemy;
        // public bool StayInRange;
        public Player FocusedTarget;
        public bool FocusPlayerOnTarget;
        public EnemyTargetCategory TargetCategory;
        public EnemyTargetCategory DefaultTargetCategory;
        public Probability<EnemyActions> ActionProbability;

        private List<Player> AllPlayers => GameController.Manager.Players.Where (t => !t.IsDead).ToList ();
        public EnemyAI (Enemy enemy, EnemyTargetCategory targetCategory, /* bool stayInRange, */
            IDictionary<EnemyActions, int> actionProbabilities = null,
            EnemyActions defaultAction = EnemyActions.SpellAttack,
            Hivemind hivemind = null) {

            Hivemind = hivemind ?? Hivemind.Global;
            TargetCategory = targetCategory;
            // StayInRange = stayInRange;

            if (actionProbabilities == null || actionProbabilities?.IsEmpty () == true) {
                ActionProbability = new Probability<EnemyActions> (new Dictionary<EnemyActions, int> () {
                    {EnemyActions.BasicAttack, 20},
                    {EnemyActions.SpellAttack, 50},
                    {EnemyActions.TargetWeakness, 20},
                    {EnemyActions.FindWeakness, 10},
                });
            } else {
                ActionProbability = new Probability<EnemyActions> (actionProbabilities);
            }

            Enemy = enemy;
        }

        public virtual (Player target, EnemyActions action, List<SpellBase> possibleSpells) NextTurnActions (EnemyActions? carryAction = null) {
            // TODO: Maybe check if target is null
            var target = FindTarget ();

            if (target == null) {
                return (target, EnemyActions.Stay, new List<SpellBase> ());
            }

            if (carryAction != null) {
                Debug.Log($"{Enemy.Name} Racalculate action! {carryAction}");
            }

            var action = carryAction ?? ActionProbability.GetResult ();

            if (action == EnemyActions.BasicAttack ||
                action == EnemyActions.Disengage ||
                action == EnemyActions.Stay) {
                return (target, action, new List<SpellBase> ());
            }

            var spells = new List<SpellBase> ();

            switch (action) {
                case EnemyActions.SpellAttack:
                    spells = Enemy.Persona.SpellBook.Spells;
                    break;
                case EnemyActions.Buff:
                    spells = Enemy.Persona.SpellBook.Spells
                        .GetSpellsFromElement (Elements.Recovery)
                        .Where (w => w is IAssitSpell || w is ISupportSpell)
                        .ToList ();
                    break;
                case EnemyActions.Debuff:
                    spells = Enemy.Persona.SpellBook.Spells
                        .GetSpellsFromElement (Elements.Ailment)
                        .ToList ();
                    break;
                case EnemyActions.TargetWeakness:
                    var weaknesses = Hivemind.CollectedInfo[target].Resistances
                        .Where (r => r.Value == ResistanceModifiers.Weak)
                        .Select (s => s.Key);

                    if (weaknesses.Count () == 0) break;

                    foreach (var weakness in weaknesses) {
                        spells.AddRange (Enemy.Persona.SpellBook.Spells
                            .GetSpellsFromElement (weakness));
                    }
                    break;
                case EnemyActions.FindWeakness:
                    var knownResistance = Hivemind.CollectedInfo[target].Resistances
                        .Select (s => s.Key);
                    var unknownResistance = EnumUtils<Elements>.ToList()
                        .Except(knownResistance);

                    if (unknownResistance.Count() == 0) break;
                    
                    foreach (var weakness in unknownResistance) {
                        spells.AddRange (Enemy.Persona.SpellBook.Spells
                            .GetSpellsFromElement (weakness));
                    }
                    break;
            }

            spells.RemoveAll (s => !s.CanBeCasted (Enemy));

            if (spells.IsEmpty ()) {
                if (action == EnemyActions.SpellAttack) {
                    return NextTurnActions (EnemyActions.BasicAttack) ;
                } 
                return NextTurnActions (EnemyActions.SpellAttack);
            }

            return (target, action, spells.ToList ());
        }

        // TODO:
        // Make it get the list of players as a parameter
        // and call function again to get default target for tie breakers
        // retun List<Player> ordered by category
        public virtual Player FindTarget () {
            if (FocusedTarget != null) {
                return FocusedTarget;
            }

            Player target = null;

            if (TargetCategory != EnemyTargetCategory.Closest &&
                Hivemind.CollectedInfo.IsEmpty ()) {

                target = FindClosest (AllPlayers);

                if (FocusPlayerOnTarget) {
                    FocusedTarget = target;
                }

                return target;
            }
            
            var targetCategory = TargetCategory;
            if (TargetCategory == EnemyTargetCategory.Random) {
                targetCategory = EnumUtils<EnemyTargetCategory>.GetValues().GetRandomValue();
            }

            switch (targetCategory) {
                case EnemyTargetCategory.Closest:
                    target = FindClosest (AllPlayers);
                    break;
                case EnemyTargetCategory.CanHitWeakness:
                    target = FindClosest (FindCanHitWeakness (true));
                    break;
                case EnemyTargetCategory.CannotHitWeakness:
                    target = FindClosest (FindCanHitWeakness (false));
                    break;
                case EnemyTargetCategory.HasWeakness:
                    target = FindClosest (FindHasWeakness (true));
                    break;
                case EnemyTargetCategory.DoesNotHaveWeakness:
                    target = FindClosest (FindHasWeakness (false));
                    break;
                case EnemyTargetCategory.HasHealingSpells:
                    var targets = Hivemind.CollectedInfo
                        .Select (s => (s.Key, s.Value))
                        .Where (w => !w.Value.Spells.GetSpellsFromElement (Elements.Recovery).IsEmpty ())
                        .Select (s => s.Key);
                    target = FindClosest (targets);
                    break;
                case EnemyTargetCategory.HighestDamaged:
                    target = Hivemind.CollectedInfo
                        .Select (s => (s.Key, s.Value))
                        .OrderByDescending (o => o.Value.HpLost)
                        .First ().Key;
                    break;
                case EnemyTargetCategory.LowestDamaged:
                    target = Hivemind.CollectedInfo
                        .Select (s => (s.Key, s.Value))
                        .OrderBy (o => o.Value.HpLost)
                        .First ().Key;
                    break;
                case EnemyTargetCategory.HighestSpUsed:
                    target = Hivemind.CollectedInfo
                        .Select (s => (s.Key, s.Value))
                        .OrderByDescending (o => o.Value.SpLost)
                        .First ().Key;
                    break;
            }

            if (FocusPlayerOnTarget) {
                FocusedTarget = target;
            }

            return target;

            List<Player> FindHasWeakness (bool has) {
                HashSet<Player> possibleTargets = new HashSet<Player> ();

                foreach (var (player, info) in Hivemind.CollectedInfo) {
                    var weaknesses = info.Resistances
                        .Where (w => w.Value == Enums.ResistanceModifiers.Weak)
                        .Select (s => s.Key);

                    foreach (var weakness in weaknesses) {
                        if (Enemy.Persona.SpellBook.Spells.GetSpellsFromElement (weakness).IsEmpty () != has) {
                            possibleTargets.Add (player);
                        }
                    }
                }
                return possibleTargets.ToList ();
            }

            List<Player> FindCanHitWeakness (bool canHit) {
                HashSet<Player> possibleTargets = new HashSet<Player> ();

                var weaknesses = Enemy.Persona.Resistances
                    .Where (w => w.Value == Enums.ResistanceModifiers.Weak)
                    .Select (s => s.Key);

                foreach (var (player, info) in Hivemind.CollectedInfo) {
                    foreach (var weakness in weaknesses) {
                        if (info.Spells.GetSpellsFromElement (weakness).IsEmpty () != canHit) {
                            possibleTargets.Add (player);
                        }
                    }
                }
                return possibleTargets.ToList ();
            }

            Player FindClosest (IEnumerable<Player> list) {
                var playerList = list;
                playerList = playerList.OrderBy( o => Enemy.DistanceFromCombatRange (o));

                return playerList.FirstOrDefault(
                    f => {
                        var allOccupied = f.Location.Neighbors.TrueForAll( t => t.IsOccupied);
                        if (!allOccupied) 
                            return true;
                        return f.Location.Neighbors.Contains(Enemy.Location);
                    }
                );
            }
        }

        public void ChangeTargetCategory (EnemyTargetCategory target) {
            TargetCategory = target;
        }

        public void SetFocusTarget (Player focusTarget) {
            FocusedTarget = focusTarget;
        }

        public void ChangeActionProbabilities (IDictionary<EnemyActions, int> actionProbabilities) {
            ActionProbability = new Probability<EnemyActions> (actionProbabilities);
        }

        public enum EnemyTargetCategory {
            Closest,
            CanHitWeakness,
            CannotHitWeakness,
            HasWeakness,
            DoesNotHaveWeakness,
            HasHealingSpells,
            HighestDamaged,
            LowestDamaged,
            HighestSpUsed,
            Random, 
        }

        public enum EnemyActions {
            BasicAttack,
            SpellAttack,
            FindWeakness,
            TargetWeakness,
            //Heal,
            Buff,
            Debuff,
            Disengage,
            Stay,
            // AllOutAttackPosition,
        }
    }
}