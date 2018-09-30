using Assets.Take_II.Scripts.EnemyManager;
using Assets.Take_II.Scripts.Enums;
using Assets.Take_II.Scripts.GameManager;
using Assets.Take_II.Scripts.PlayerManager;
using Assets.Take_II.Scripts.Spells;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;

namespace Assets.Take_II.Scripts.Combat {
    public sealed class CombatManager {
        public static CombatManager Manager { get; } = new CombatManager();

        private CombatManager() { }

        public void BasicAttack(Character attacker, Character defender)
        {
            if (attacker == null || defender == null) return;
            
            var attackPower = 0;
            if (attacker is Player)
            {
                attackPower = ((Player) attacker).Equipment.AttackPower;
            }
            else if (attacker is Enemy)
            {
                attackPower = ((Enemy) attacker).BasicAttack;
            }
            
            var player = defender as Player;
            var damage = player != null ? Attack(attacker, player, attackPower, true) : 
                Attack(attacker, defender, attackPower, true);

            
            var resistances = defender.Stats.Resistances;
            switch (resistances[Elements.Physical])
            {
                case Resistances.Resist:
                    defender.CurrentHealth -= Mathf.RoundToInt(damage / 2f);
                    break;
                case Resistances.Repel:
                    attacker.CurrentHealth -= damage;
                    break;
                case Resistances.Drain:
                    defender.CurrentHealth += damage;
                    break;
                case Resistances.None:
                    defender.CurrentHealth -= damage;
                    break;
                case Resistances.Null:
                    break;
            }
            defender.IsSurrounded = true;
            Debug.Log($"Basic Attack: {attacker.Name} vs {defender.Name} - Damage: {damage}");
        }

        public void AllOutAttack(Character defender) {
            if (!defender.IsSurrounded && defender is Enemy) return;

            var players = defender.Location.Neighbors.Select(t => t.OccupiedBy as Player)
                                                    .Where(p => p != null ).ToList();
            
            if (players.Count != defender.Location.Neighbors.Count) return;
            
            var damage = players.Sum(a => 
                AllmightyAttack(a, defender, a.Equipment.AttackPower)
            ); 
            
            Debug.Log($"All Out Attack {defender.Name} - Damage: {damage}");
            defender.CurrentHealth -= damage;
        }

        public int PhysicalAttack(Character attacker, Character defender, Spell spell)
        {
            var player = defender as Player;
            return player != null ? Attack(attacker, player, spell.Power, true) : 
                Attack(attacker, defender, spell.Power, true);
        }

        public int MagicalAttack(Character attacker, Character defender, Spell spell) {
            var player = defender as Player;
            return player != null ? Attack(attacker, player, spell.Power, false) :
                Attack(attacker, defender, spell.Power, false);
        }

        public static int AllmightyAttack(Character attacker, Character defender, int attackPower) {
            float attackStat =  attacker.Stats.Magic;
            attackStat *= attacker.Stats.AttackBuff ? 2f : 1f;
            var netdamage = Mathf.Sqrt(attackStat * attackPower);
            var damage = netdamage * AttackVariance(attacker.Stats.Luck);
            return Mathf.RoundToInt(damage);
        }

        private static int Attack(Character attacker, Player defender, int attackPower, bool isPhysical) {
            float attackStat =  isPhysical ? attacker.Stats.Strength : attacker.Stats.Magic;
            attackStat *= attacker.Stats.AttackBuff ? 2f : 1f;
            float endurance = defender.Equipment.Armor + defender.Stats.Endurance * 8;
            endurance *= defender.Stats.DefenceBuff ? 2f : 1f;
            var netdamage = Mathf.Sqrt((attackStat/endurance) * attackPower);
            var damage = netdamage * AttackVariance(attacker.Stats.Luck);
            return Mathf.RoundToInt(damage);
        }

        private static int Attack(Character attacker, Character defender, int attackPower, bool isPhysical)
        {
            float attackStat = isPhysical ? attacker.Stats.Strength : attacker.Stats.Magic;
            attackStat *= attacker.Stats.AttackBuff ? 2f : 1f;
            float endurance = defender.Stats.Endurance * 8;
            endurance *= defender.Stats.DefenceBuff ? 2f : 1f;
            var netdamage = Mathf.Sqrt((attackStat / endurance) * attackPower);
            var damage = netdamage * AttackVariance(attacker.Stats.Luck);
            return Mathf.RoundToInt(damage);
        }

        private static float AttackVariance(int luck)
        {
            var random = Random.Range(0.95f, 1.06f);
            if (!(Random.value * 100 <= luck)) return random;

            var newRandom = Random.Range(0.95f, 1.06f);
            random = Mathf.Max(random, newRandom);
            return random;
        }
    }
}