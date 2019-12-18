using System.Linq;
using Assets.Enums;
using Assets.Spells;
using Assets.EnemySystem;
using Assets.PlayerSystem;
using Assets.CharacterSystem;
using Asstes.CharacterSystem.StatusEffects;
using Assets.UI;
using UnityEngine;

namespace Assets.CombatSystem {
    public sealed class CombatManager {
        public static CombatManager Manager { get; } = new CombatManager ();

        private CombatManager () { }

        public void BasicAttack (Character attacker, Character defender) {
            if (attacker == null || defender == null) return;
            if (!attacker.IsInMeleeRange (defender)) return;

            var attackPower = 0;
            var accuracy = 0f;
            switch (attacker) {
                case Player playerAttacker:
                    attackPower = playerAttacker.Equipment.AttackPower;
                    accuracy = playerAttacker.Equipment.Accuracy;
                    break;
                case Enemy enemyAttacker:
                    attackPower = enemyAttacker.BasicAttack;
                    accuracy = enemyAttacker.Accuracy;
                    break;
            }

            if (!SpellDidHit(attacker, defender, accuracy/100f)) {
                attacker.StatusEffect.SetStatusEffect(StatusCondition.Down);
                attacker.TurnFinished = true;
                UIFloatingText.CreateMiss(defender.gameObject);
                return;
            }

            var damage = BasicAttackDamageCalculation (attacker, defender, attackPower);

            var critChance = Mathf.Sqrt (attacker.Persona.Luck / defender.Persona.Luck) * 5f;

            var didCritical = Random.Range (1, 100) <= critChance;

            if (defender is Enemy enemy && enemy.IsBoss) {
                didCritical = false;
            }

            if (didCritical) {
                damage = Mathf.CeilToInt (damage * 1.6f);
            }

            if (defender.StatusEffect == StatusCondition.Down ||
                defender.StatusEffect == StatusCondition.Dizzy) {
                damage = Mathf.CeilToInt (damage * 1.3f);
            }

            var resolvedDamage = ResolveResistances (attacker, defender, Elements.Physical, damage);

            if (defender is Player playerDefender) {
                (attacker as Enemy).AI.Hivemind.CaptureInfoWhenBasicAttacking(playerDefender, resolvedDamage);
            } else if (attacker is Player playerAttacker) {
                (defender as Enemy).AI.Hivemind.CaptureInfoWhenBasicAttacked(playerAttacker, resolvedDamage);
            }

            attacker.DeactivateOneMore ();
            attacker.TurnFinished = true;

            var resistance = defender.Persona.Resistances[Elements.Physical];
            if (resistance == ResistanceModifiers.Weak || didCritical) {
                if (defender.StatusEffect == StatusCondition.Down) {
                    defender.StatusEffect.SetStatusEffect (StatusCondition.Dizzy);
                } else {
                    defender.StatusEffect.SetStatusEffect (StatusCondition.Down);
                    attacker.AddOneMore ();
                }
            }
        }

        public (bool oneMore, bool SpellDidHit) SpellAttack (Character attacker, Character defender, OffensiveSpell spell) {
            if (attacker == null || defender == null) return (false, false);
            if (!attacker.IsInRange (defender)) return (false, false);
            if (!spell.CanBeCasted (attacker)) return (false, false);
            if (!SpellDidHit (attacker, defender, spell.Accuracy)) {
                UIFloatingText.CreateMiss(defender.gameObject);
                return (false, false);
            }

            var didCritical = false;
            var damage = 0;
            switch (spell) {
                case PhysicalSpell physicalSpell:

                    var cannotBeCritacllyHit = false;
                    if (defender is Enemy enemy && (enemy.IsBoss 
                        || enemy.Persona.Resistances[Elements.Physical] <= ResistanceModifiers.Resist)) {
                        cannotBeCritacllyHit = true;
                    }

                    var criticalModifier = attacker.Persona.AptPupil ? 1.5 : 1;

                    for (var i = 0; i < physicalSpell.HitCount; i++) {
                       var  partialDamage = CalculateSpellDamage (attacker, defender, physicalSpell);

                        didCritical = Random.value <= (physicalSpell.CriticalChance * criticalModifier);

                        if (didCritical && !cannotBeCritacllyHit) {
                            partialDamage = Mathf.CeilToInt (partialDamage * 1.6f);
                            UIFloatingText.Create("Critical Hit!", defender.gameObject);
                        }
                            
                        damage += partialDamage;
                    }

                    break;
                case var almightySpell when almightySpell.Element == Elements.Almighty:
                    damage = AlmightyAttack (attacker, defender, almightySpell.AttackPower);
                break;
                default:
                    damage = CalculateSpellDamage (attacker, defender, spell);
                    didCritical = defender.Persona.Resistances[spell.Element] == ResistanceModifiers.Weak;
                    break;
            }

            if (defender.StatusEffect == StatusCondition.Down ||
                defender.StatusEffect == StatusCondition.Dizzy) {
                damage = Mathf.CeilToInt (damage * 1.3f);
            }

            var resolvedDamage = ResolveResistances (attacker, defender, spell.Element, damage);
            
            if (defender is Player playerDefender) {
                (attacker as Enemy).AI.Hivemind.CaptureInfoWhenAttacking(playerDefender, spell, resolvedDamage);
            } else if (attacker is Player playerAttacker) {
                (defender as Enemy).AI.Hivemind.CaptureInfoWhenAttacked(playerAttacker, spell);
            }
            return (didCritical, true);
        }

        public void AllOutAttack (Character defender) {
            if (!defender.IsSurrounded && defender is Enemy) return;

            var players = defender.Location.Neighbors.Select (t => t.Occupant as Player)
                .Where (p => p != null).ToList ();

            if (players.Count != defender.Location.Neighbors.Count) return;

            var damage = players.Sum (a =>
                AlmightyAttack (a, defender, a.Equipment.AttackPower)
            );

            if (defender.StatusEffect == StatusCondition.Down ||
                defender.StatusEffect == StatusCondition.Dizzy) {
                damage = Mathf.CeilToInt (damage * 1.3f);
            }

            UIFloatingText.Create($"AOA! {damage}", defender.gameObject, Elements.Almighty);
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

        // TODO: Take in consideration chances for evasion
        public static bool SpellDidHit (Character attacker, Character defender, float accuracy) {
            var firstChanceToHit = Random.value <= accuracy;
            if (firstChanceToHit) return true;
            var tryAgain = Random.value * 100 <= attacker.Persona.Agility;
            if (!tryAgain) return false;
            var secondChanceToHit = Random.value <= accuracy;
            return secondChanceToHit;
        }

        private int CalculateSpellDamage (Character attacker, Character defender, OffensiveSpell spell) {
            return Attack (attacker, defender, spell);
        }

        private static int AlmightyAttack (Character attacker, Character defender, int attackPower) {
            var attackStat = attacker.Persona.Magic;
            var modifier = CalculateDamageModifier (attacker, defender, Elements.Almighty);
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

        private static int Attack (Character attacker, Character defender, OffensiveSpell spell) {
            var isPhysical = spell.Element == Elements.Physical;
            var armorModifier = defender is Player player ? (player.Equipment.Armor) : 0;

            float attackStat = isPhysical ? attacker.Persona.Strength : attacker.Persona.Magic;
            var defenceStat = armorModifier + defender.Persona.Endurance * 8;
            var modifier = CalculateDamageModifier (attacker, defender, spell.Element);
            var netdamage = 5 * Mathf.Sqrt ((attackStat / defenceStat) * spell.AttackPower) * modifier;
            var damage = netdamage * PowerVariance (attacker.Persona.Luck);
            return Mathf.CeilToInt (damage);
        }

        private static float CalculateDamageModifier (Character attacker, Character defender, Elements element = Elements.None) {
            var modifier = 1f;
          
            if (element == Elements.None) {
                return modifier;
            }

            if (element == Elements.Physical) {
                if (attacker.Persona.PowerCharged) {
                    modifier *= 2.5f;
                    attacker.Persona.PowerCharged = false;
                }
            } else if (attacker.Persona.MindCharged) {
                modifier *= 2.5f;
                attacker.Persona.MindCharged = false;
            }
            
            modifier *= attacker.Persona.ElementDamageModifier[element];

            return modifier;
        }

        private int ResolveResistances (Character attacker, Character defender, Elements element, int damage, bool canReflect = true) {
           if (attacker.Persona.CounterChance > 0 && canReflect) {
                var chance  = Random.value;
                if (chance <=  attacker.Persona.CounterChance) {
                    UIFloatingText.Create("Reflected", attacker.gameObject, Elements.Recovery);
                    return ResolveResistances(defender, attacker, element, damage, false);
                } 
           }
           
           var finalDamage = damage;
            switch (defender.Persona.Resistances[element]) {
                 case ResistanceModifiers.None:
                    defender.CurrentHP -= damage;
                    break;
                case ResistanceModifiers.Resist:
                    finalDamage = Mathf.CeilToInt (damage * 0.6f);
                    defender.CurrentHP -= finalDamage;
                    break;
                case ResistanceModifiers.Weak:
                    finalDamage = Mathf.CeilToInt (damage * 1.6f);
                    defender.CurrentHP -= finalDamage;
                    break;

                case ResistanceModifiers.Block:
                    UIFloatingText.Create("Blocked", defender.gameObject, Elements.Recovery);
                    return 0;
                case ResistanceModifiers.Absorb:
                    defender.CurrentHP += damage;
                    UIFloatingText.Create("Absorbed", defender.gameObject, Elements.Recovery);
                    UIFloatingText.Create($"+{damage}", defender.gameObject, Elements.Recovery);
                    return 0;
                case ResistanceModifiers.Reflect:
                    if (canReflect) {
                        UIFloatingText.Create("Reflected", attacker.gameObject, Elements.Recovery);
                        ResolveResistances(defender, attacker, element, damage, false);
                    } else {
                        UIFloatingText.Create("Blocked", attacker.gameObject, Elements.Recovery);
                    }
                    return 0;
            }

            UIFloatingText.Create($"{finalDamage}", defender.gameObject, element);
            return finalDamage;
        }
    }
}