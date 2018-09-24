using System;
using Assets.Take_II.Scripts.Enums;
using Assets.Take_II.Scripts.PlayerManager;
using Assets.Take_II.Scripts.Spells;
using GooglePlayServices;
using NUnit.Framework;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Take_II.Scripts.Combat {
    public sealed class CombatManager {
        public static CombatManager Manager { get; } = new CombatManager();

        private CombatManager() { }

        public void BasicAttack(Player attacker, Player defender)
        {
            if (attacker == null || defender == null) return;

            var damage = Attack(attacker, defender, attacker.Equipment.AttackPower, true);

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
        }

        public int PhysicalAttack(Player attacker, Player defender, Spell spell) {
            return Attack(attacker, defender, spell.Power, true);
        }

        public int MagicalAttack(Player attacker, Player defender, Spell spell) {
            return Attack(attacker, defender, spell.Power, false);
        }

        private static int Attack(Player attacker, Player defender, int attackPower, bool isPhysical) {
            float attackStat =  isPhysical ? attacker.Stats.Strength : attacker.Stats.Magic;
            attackStat *= attacker.Stats.AttackBuff ? 2f : 1f;
            float endurance = defender.Equipment.Armor + defender.Stats.Endurance * 8;
            endurance *= defender.Stats.DefenceBuff ? 2f : 1f;
            var netdamage = Mathf.Sqrt((attackStat/endurance) * attackPower);
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