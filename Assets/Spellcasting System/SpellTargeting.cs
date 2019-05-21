using System.Collections.Generic;
using Assets.EnemySystem;
using Assets.GameSystem;
using Assets.PlayerSystem;
using Assets.Spells;

namespace Assets.SpellCastingSystem {
    public class SpellTargeting  {
        public List<Character> GetPossibleTargets(Character attacker, SpellBase spell) {
                var targets = new List<Character>();
                var neighbors = attacker.Location.Neighbors;
                
                foreach(var neighbor in neighbors) {
                    var target = neighbor.Occupant;
                    if (target == null) {
                        continue;
                    }    

                    if (spell is OffensiveSpell && target is Enemy) {
                        targets.Add(target);
                        continue;
                    }

                    if (spell is SupportSpell && target is Player) {
                        targets.Add(target);
                    }
                }

                return targets;
            }
    }
}

