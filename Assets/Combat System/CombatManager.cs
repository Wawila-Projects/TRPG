using Assets.EnemySystem;
using Assets.Enums;
using Assets.CharacterSystem;
using Assets.PlayerSystem;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;
using Assets.Spells;
using Asstes.CharacterSystem;

namespace Assets.CombatSystem {
    public sealed class CombatManager {
        public static CombatManager Manager { get; } = new CombatManager ();

        private CombatManager () { }

        public void BasicAttack (Character attacker, Character defender) {
            if (attacker == null || defender == null) return;
            if (!attacker.IsInRange (defender)) return;
            // TODO Check for miss attacks

            var attackPower = 0;
            switch (attacker) {
                case Player playerAttacker:
                    attackPower = playerAttacker.Equipment.AttackPower;
                    break;
                case Enemy enemyAttacker:
                    attackPower = enemyAttacker.BasicAttack;
                    break;
            }

            var damage = BasicAttackDamageCalculation (attacker, defender, attackPower);

            if (defender.StatusEffect == StatusConditions.Down || 
                defender.StatusEffect == StatusConditions.Dizzy) {
                damage = Mathf.CeilToInt(damage * 1.3f);
            }

            var (resolvedDamage, result) = ResolveResistances (attacker, defender, Elements.Physical, damage);

            attacker.DeactivateOneMore();
            attacker.TurnFinished = true;
            Debug.Log ($"Basic Attack: {attacker.Name} vs {defender.Name} - {result}: {resolvedDamage}");
        }

        // TODO: Fix spell damage. Defensse vs Attack
        public ResistanceModifiers? SpellAttack (Character attacker, Character defender, OffensiveSpell spell) {
            if (attacker == null || defender == null) return null;
            if (!attacker.IsInRange (defender)) return null;
            if (!spell.CanBeCasted (attacker)) return null;
            if (!SpellDidHit (attacker, defender, spell.Accuracy)) {
                Debug.Log ($"{spell.Name}: {attacker.Name} vs {defender.Name} - Missed!");
                return null;
            }

            int damage = 0;
            switch (spell) {
                // TODO: Add Critical Chance and Critical on weak
                case PhysicalSpell physicalSpell:
                    for (var i = 0; i < physicalSpell.HitCount; i++) {
                        damage += PhysicalAttack (attacker, defender, physicalSpell);
                    }
                    break;
                case var almightySpell when almightySpell.Element == Elements.Almighty:
                    damage = AlmightyAttack (attacker, defender, almightySpell.AttackPower);
                break;
                default:
                    damage = MagicalAttack (attacker, defender, spell);
                    break;
            }

            if (defender.StatusEffect == StatusConditions.Down || 
                defender.StatusEffect == StatusConditions.Dizzy) {
                damage = Mathf.CeilToInt(damage * 1.3f);
            }

            var (resolvedDamage, result) = ResolveResistances (attacker, defender, spell.Element, damage);
            
            Debug.Log ($"{spell.Name}: {attacker.Name} vs {defender.Name} - {result}: {resolvedDamage}");
            return defender.Persona.Resistances[spell.Element];
        }

        public void AllOutAttack (Character defender) {
            if (!defender.IsSurrounded && defender is Enemy) return;

            var players = defender.Location.Neighbors.Select (t => t.Occupant as Player)
                .Where (p => p != null).ToList ();

            if (players.Count != defender.Location.Neighbors.Count) return;

            var damage = players.Sum (a =>
                AlmightyAttack (a, defender, a.Equipment.AttackPower)
            );

            if (defender.StatusEffect == StatusConditions.Down || 
                defender.StatusEffect == StatusConditions.Dizzy) {
                damage = Mathf.CeilToInt(damage * 1.3f);
            }

            Debug.Log ($"All Out Attack {defender.Name} - Damage: {damage}");
            defender.IsSurrounded = true;
            defender.CurrentHP -= damage;
        }

          public static float PowerVariance (int luck) {
            var random = Random.Range (0.95f, 1.06f);
            if (Random.value * 100 > luck) return random;

            var newRandom = Random.Range (0.95f, 1.06f);
            random = Mathf.Max (random, newRandom);
            return random;
        }

        // TODO: Take in consideration chances for evation
        public static bool SpellDidHit (Character attacker, Character defender, float accuracy) {
            var firstChanceToHit = Random.value <= accuracy;
            if (firstChanceToHit) return true;
            var tryAgain = Random.value * 100 > attacker.Persona.Agility;
            if (tryAgain) return false;
            var secondChanceToHit = Random.value <= accuracy;
            return secondChanceToHit;
        }

        private int PhysicalAttack (Character attacker, Character defender, PhysicalSpell spell) {
            if (defender is Player player) {
                return Attack (attacker, player, spell.AttackPower, true);
            }
            return Attack (attacker, defender, spell.AttackPower, true);
        }

        private int MagicalAttack (Character attacker, Character defender, OffensiveSpell spell) {
            if (defender is Player player) {
                return Attack (attacker, player, spell.AttackPower, false);
            }
            return Attack (attacker, defender, spell.AttackPower, false);
        }

        private static int AlmightyAttack (Character attacker, Character defender, int attackPower) {
            var attackStat = attacker.Persona.Magic;
            var modifier = CalculateDamageModifier (attacker, defender, false);
            var netdamage = Mathf.Sqrt (attackStat * attackPower) * modifier;
            var damage = netdamage * PowerVariance (attacker.Persona.Luck);
            return Mathf.CeilToInt (damage);
        }

        private static int BasicAttackDamageCalculation (Character attacker, Character defender, int attackPower) {
            var modifier = CalculateDamageModifier (attacker, defender);
            var netdamage = Mathf.Sqrt (attackPower * attacker.Persona.Strength) * modifier;
            var damage = netdamage * PowerVariance (attacker.Persona.Luck);
            return Mathf.CeilToInt (damage);
        }

        private static int Attack (Character attacker, Player defender, int attackPower, bool isPhysical) {
            float attackStat = isPhysical ? attacker.Persona.Strength : attacker.Persona.Magic;
            var defenceStat = defender.Equipment.Armor + defender.Persona.Endurance * 8f;
            var modifier = CalculateDamageModifier (attacker, defender, isPhysical);
            var netdamage = Mathf.Sqrt ((attackStat / defenceStat) * attackPower) * modifier;
            var damage = netdamage * PowerVariance (attacker.Persona.Luck);
            return Mathf.CeilToInt (damage);
        }

        private static int Attack (Character attacker, Character defender, int attackPower, bool isPhysical) {
            float attackStat = isPhysical ? attacker.Persona.Strength : attacker.Persona.Magic;
            var defenceStat = defender.Persona.Endurance;
            var modifier = CalculateDamageModifier (attacker, defender, isPhysical);
            var netdamage = 5 * Mathf.Sqrt ((attackStat / defenceStat) * attackPower) * modifier;
            var damage = netdamage * PowerVariance (attacker.Persona.Luck);
            return Mathf.CeilToInt (damage);
        }

        private static float CalculateDamageModifier (Character attacker, Character defender, bool? isPhysical = null) {
            var modifier = 1f;
            if (attacker.Persona.AttackBuff == StatsModifiers.Buff) {
                modifier *= 1.3f;
            } else if (attacker.Persona.AttackBuff == StatsModifiers.Debuff) {
                modifier /= 1.3f;
            }

            if (defender.Persona.DefenceBuff == StatsModifiers.Buff) {
                modifier /= 1.3f;
            } else if (defender.Persona.DefenceBuff == StatsModifiers.Debuff) {
                modifier *= 1.3f;
            }

            if (isPhysical == null) {
                return modifier;
            }

            if (isPhysical == true) {
                if (attacker.Persona.PowerCharged) {
                    modifier *= 2.5f;
                }
            } else if (attacker.Persona.MindCharged) {
                modifier *= 2.5f;
            }

            // TODO: Spell element modifiers. Amps, buffs and accesorries 
            return modifier;
        }

        private  (int damage, string result) ResolveResistances (Character attacker, Character defender, Elements element, int damage) {
            switch (defender.Persona.Resistances[element]) {
                case ResistanceModifiers.Resist:
                    var resisted = Mathf.CeilToInt (damage * 0.8f);
                    defender.CurrentHP -= resisted;
                   return (resisted, "Damage");
                case ResistanceModifiers.Weak:
                    var aumented = Mathf.CeilToInt (damage * 1.8f);
                    defender.CurrentHP -= aumented;
                    return (aumented, "Damage");
                case ResistanceModifiers.Reflect:
                    attacker.CurrentHP -= damage;
                    return (0, "Reflected");
                case ResistanceModifiers.Absorb:
                    defender.CurrentHP += damage;
                    return (0, "Absorbed");
                case ResistanceModifiers.None:
                    defender.CurrentHP -= damage;
                    return (damage, "Damage");
                case ResistanceModifiers.Block:
                    return (0, "Blocked");
            }
            return (0, "");
        }
    }
}