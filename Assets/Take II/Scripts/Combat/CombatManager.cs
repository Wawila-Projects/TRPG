using Assets.Take_II.Scripts.PlayerManager;
using UnityEngine;

namespace Assets.Take_II.Scripts.Combat {
    public class CombatManager {
        public CombatManager Manager { get; } = new CombatManager();

        public int BasicAttack(Player attacker, Player defender, int attackPower) {
            return Attack(attacker, defender, attackPower, true);
        }

        public int PhysicalAttack(Player attacker, Player defender, int attackPower) {
            return Attack(attacker, defender, attackPower, true);
        }

        public int MagicalAttack(Player attacker, Player defender, int attackPower) {
            return Attack(attacker, defender, attackPower, false);
        }

        private int Attack(Player attacker, Player defender, int attackPower, bool isPhysical) {
            var attackStat = isPhysical ? attacker.Stats.Strength : attacker.Stats.Magic;
            attackStat *= attacker.Stats.AttackBuff ? 2 : 1;
            var endurance = defender.Stats.Endurance; // + armor * 8;
            endurance *= defender.Stats.DefenceBuff ? 2 : 1;
            var netdamage = Mathf.Sqrt((attackStat/endurance) * attackPower);
            var damage = netdamage * Random.Range(0.95f, 1.06f);
            return Mathf.RoundToInt(damage);
        }
    }
}