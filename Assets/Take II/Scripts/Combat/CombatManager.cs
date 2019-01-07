using Assets.Take_II.Scripts.EnemyManager;
using Assets.Take_II.Scripts.Enums;
using Assets.Take_II.Scripts.GameManager;
using Assets.Take_II.Scripts.PlayerManager;
using Assets.Take_II.Scripts.Spells;
// ? Quitar unity dependecy 
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;
using Assets.Spells;

namespace Assets.Take_II.Scripts.Combat {
    public sealed class CombatManager {
        public static CombatManager Manager { get; } = new CombatManager();

        private CombatManager() { }

        public void BasicAttack(Character attacker, Character defender) {
            if (attacker == null || defender == null) return;
            if (!attacker.IsInCombatRange(defender)) return;
            
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
            ResolveResistances(attacker, defender, Elements.Physical, damage);

            attacker.TurnFinished = true;
            Debug.Log($"Basic Attack: {attacker.Name} vs {defender.Name} - Damage: {damage}");
        }

        public void SpellAttack(Character attacker, OffensiveSpell spell) {
            if (!spell.IsMultitarget) {
                return;
            }
            foreach (var tile in attacker.Location.Neighbors) {
                if (tile.OccupiedBy ==  null) continue;
                if (tile.OccupiedBy is Enemy) 
                    SpellAttack(attacker, tile.OccupiedBy, spell);
            }
        }

        public void SpellAttack(Character attacker, Character defender, OffensiveSpell spell) {
            if (attacker == null || defender == null) return;
            if (!attacker.IsInCombatRange(defender)) return;
            if (SpellDidHit(attacker, defender, spell)) return;

            int damage;
            if (spell.Element == Elements.Physical) {
                damage = PhysicalAttack(attacker, defender, spell);
            } else {
                damage = MagicalAttack(attacker, defender, spell);
            }
            ResolveResistances(attacker, defender, spell.Element, damage);
            attacker.TurnFinished = true;
            Debug.Log($"{spell.Name}: {attacker.Name} vs {defender.Name} - Damage: {damage}");
        }

        public void AllOutAttack(Character defender) {
            if (!defender.IsSurrounded && defender is Enemy) return;

            var players = defender.Location.Neighbors.Select(t => t.OccupiedBy as Player)
                                                    .Where(p => p != null ).ToList();
            
            if (players.Count != defender.Location.Neighbors.Count) return;
            
            var damage = players.Sum(a => 
                AlmightyAttack(a, defender, a.Equipment.AttackPower)
            ); 
            
            Debug.Log($"All Out Attack {defender.Name} - Damage: {damage}");
            defender.IsSurrounded = true;
            defender.CurrentHealth -= damage;
        }

        public int PhysicalAttack(Character attacker, Character defender, OffensiveSpell spell) {
            var player = defender as Player;
            return player != null ? Attack(attacker, player, spell.AttackPower, true) : 
                Attack(attacker, defender, spell.AttackPower, true);
        }

        public int MagicalAttack(Character attacker, Character defender, OffensiveSpell spell) {
            var player = defender as Player;
            return player != null ? Attack(attacker, player, spell.AttackPower, false) :
                Attack(attacker, defender, spell.AttackPower, false);
        }

        public static int AlmightyAttack(Character attacker, Character defender, int attackPower) {
            var attackStat =  attacker.Stats.Magic;
            var modifier = CalculateDamageModifier(attacker, defender, false);
            var netdamage = Mathf.Sqrt(attackStat * attackPower) * modifier;
            var damage = netdamage * AttackVariance(attacker.Stats.Luck);
            return Mathf.RoundToInt(damage);
        }

        private static int Attack(Character attacker, Player defender, int attackPower, bool isPhysical) {
            var attackStat = isPhysical ? attacker.Stats.Strength : attacker.Stats.Magic;
            var defenceStat = defender.Equipment.Armor + defender.Stats.Endurance * 8;
            var modifier = CalculateDamageModifier(attacker, defender, isPhysical);
            var netdamage = Mathf.Sqrt((attackStat / defenceStat) * attackPower) * modifier;
            var damage = netdamage * AttackVariance(attacker.Stats.Luck);
            return Mathf.RoundToInt(damage);
        }

        private static int Attack(Character attacker, Character defender, int attackPower, bool isPhysical) {
            var attackStat = isPhysical ? attacker.Stats.Strength : attacker.Stats.Magic;
            var defenceStat = defender.Stats.Endurance * 8;
            var modifier = CalculateDamageModifier(attacker, defender, isPhysical);
            var netdamage = Mathf.Sqrt((attackStat / defenceStat) * attackPower) * modifier;
            var damage = netdamage * AttackVariance(attacker.Stats.Luck);
            return Mathf.RoundToInt(damage);
        }

        // TODO: Take in consideration chances for evation
        private static bool SpellDidHit(Character attacker, Character defender, OffensiveSpell spell) {
            var firstChanceToHit = Random.value * 100 <= spell.Accuracy;
            if (Random.value * 100 > attacker.Stats.Agility) return firstChanceToHit;
            var secondChanceToHit = Random.value * 100 <= spell.Accuracy;
            return firstChanceToHit || secondChanceToHit;
        }

        private static float CalculateDamageModifier(Character attacker, Character defender, bool isPhysical) {
            var modifier = 1f;
            if (attacker.Stats.AttackBuff == StatsModifiers.Buff) {
                 modifier *= 1.3f;
            }  
            else if (attacker.Stats.AttackBuff == StatsModifiers.Debuff) {
                modifier /= 1.3f;
            } 
            
            if (defender.Stats.DefenceBuff == StatsModifiers.Buff) {
                modifier /= 1.3f;
            }
            else if (defender.Stats.DefenceBuff == StatsModifiers.Debuff) {
                modifier *= 1.3f;
            }

            if (isPhysical) {
                if (attacker.Stats.PowerCharged) {
                    modifier *= 2.5f;
                }
            } else if (attacker.Stats.MindCharged) {
                modifier *= 2.5f;
            }

            // TODO: Spell element modifiers. Amps, buffs and accesorries 
            return modifier;
        }

        private static float AttackVariance(int luck) {
            var random = Random.Range(0.95f, 1.06f);
            if (Random.value * 100 > luck) return random;

            var newRandom = Random.Range(0.95f, 1.06f);
            random = Mathf.Max(random, newRandom);
            return random;
        }

        private static void ResolveResistances(Character attacker, Character defender, Elements element, int damage) {
            switch (defender.Stats.Resistances[element])
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
    }
}