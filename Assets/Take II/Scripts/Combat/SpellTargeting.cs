using Assets.Take_II.Scripts.PlayerManager;
using Assets.Take_II.Scripts.EnemyManager;
using Assets.Take_II.Scripts.GameManager;
using Assets.Spells;
using System.Collections.Generic;

namespace Assets.Take_II.Scripts.Combat {
    public sealed class SpellManager {
        public static SpellManager Manager { get; } = new SpellManager(); 
        private SpellManager() { }

        public Player Attacker;


        public List<Character> GetPossibleTarget(SpellBase spell) {
            var targets = new List<Character>();
            var neighbors = Attacker.Location.Neighbors;
            
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