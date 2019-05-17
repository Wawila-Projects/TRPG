using Assets.Take_II.Scripts.EnemyManager;
using Assets.Take_II.Scripts.Enums;
using Assets.Take_II.Scripts.GameManager;
using Assets.Take_II.Scripts.PlayerManager;
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
            // TODO Check for miss attacks

            var attackPower = 0;
            switch(attacker) {
                case Player playerAttacker: 
                    attackPower = playerAttacker.Equipment.AttackPower;
                    break;
                case Enemy enemyAttacker:
                    attackPower = enemyAttacker.BasicAttack;
                    break;
            }   

            var damage = BasicAttackDamageCalculation(attacker, defender, attackPower);
            attacker.TurnFinished = true;
            Debug.Log($"Basic Attack: {attacker.Name} vs {defender.Name} - Damage: {damage}");
        }

        public void MultitargetSpellAttack(Character attacker, OffensiveSpell spell) {
            if (!spell.IsMultitarget) {
                return;
            }
            foreach (var tile in attacker.Location.Neighbors) {
                if (!tile.IsOccupied) continue;
                if (tile.Occupant is Enemy) 
                    SpellAttack(attacker, tile.Occupant, spell);
            }
        }

        public void SpellAttack(Character attacker, Character defender, OffensiveSpell spell) {
            if (attacker == null || defender == null) return;
            if (!attacker.IsInCombatRange(defender)) return;
            if (!spell.CanBeCasted(attacker)) return;
            if (SpellDidHit(attacker, defender, spell)) return;

            int damage = 0;
            switch(spell) {
                case PhysicalSpell physicalSpell:
                for (var i = 0; i < physicalSpell.HitCount; i++) {
                    damage += PhysicalAttack(attacker, defender, physicalSpell);
                }
                break;
                case var almightySpell when almightySpell.Element == Elements.Almighty:
                    damage = AlmightyAttack(attacker, defender, almightySpell.AttackPower);
                break;
                default:
                    damage = MagicalAttack(attacker, defender, spell);
                break;
            }

            ResolveResistances(attacker, defender, spell.Element, damage);
            spell.HandleCostReduction(attacker);
            attacker.TurnFinished = true;
            Debug.Log($"{spell.Name}: {attacker.Name} vs {defender.Name} - Damage: {damage}");
        }

        public void AllOutAttack(Character defender) {
            if (!defender.IsSurrounded && defender is Enemy) return;

            var players = defender.Location.Neighbors.Select(t => t.Occupant as Player)
                                                    .Where(p => p != null ).ToList();
            
            if (players.Count != defender.Location.Neighbors.Count) return;
            
            var damage = players.Sum(a => 
                AlmightyAttack(a, defender, a.Equipment.AttackPower)
            ); 
            
            Debug.Log($"All Out Attack {defender.Name} - Damage: {damage}");
            defender.IsSurrounded = true;
            defender.CurrentHealth -= damage;
        }

        public int PhysicalAttack(Character attacker, Character defender, PhysicalSpell spell) {
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
            var attackStat =  attacker.Persona.Magic;
            var modifier = CalculateDamageModifier(attacker, defender, false);
            var netdamage = Mathf.Sqrt(attackStat * attackPower) * modifier;
            var damage = netdamage * AttackVariance(attacker.Persona.Luck);
            return Mathf.RoundToInt(damage);
        }

        private static int BasicAttackDamageCalculation(Character attacker, Character defender, int attackPower) {
            var modifier = CalculateDamageModifier(attacker, defender);
            var netdamage = Mathf.Sqrt(attackPower * attacker.Persona.Strength) * modifier;
            var damage = netdamage * AttackVariance(attacker.Persona.Luck);
            return Mathf.RoundToInt(damage);
        }

        private static int Attack(Character attacker, Player defender, int attackPower, bool isPhysical) {
            var attackStat = isPhysical ? attacker.Persona.Strength : attacker.Persona.Magic;
            var defenceStat = defender.Equipment.Armor + defender.Persona.Endurance * 8;
            var modifier = CalculateDamageModifier(attacker, defender, isPhysical);
            var netdamage = Mathf.Sqrt((attackStat / defenceStat) * attackPower) * modifier;
            var damage = netdamage * AttackVariance(attacker.Persona.Luck);
            return Mathf.RoundToInt(damage);
        }

        private static int Attack(Character attacker, Character defender, int attackPower, bool isPhysical) {
            var attackStat = isPhysical ? attacker.Persona.Strength : attacker.Persona.Magic;
            var defenceStat = defender.Persona.Endurance * 8;
            var modifier = CalculateDamageModifier(attacker, defender, isPhysical);
            var netdamage = Mathf.Sqrt((attackStat / defenceStat) * attackPower) * modifier;
            var damage = netdamage * AttackVariance(attacker.Persona.Luck);
            return Mathf.RoundToInt(damage);
        }

        // TODO: Take in consideration chances for evation
        private static bool SpellDidHit(Character attacker, Character defender, OffensiveSpell spell) {
            var firstChanceToHit = Random.value <= spell.Accuracy;
            if (Random.value * 100 > attacker.Persona.Agility) return firstChanceToHit;
            var secondChanceToHit = Random.value <= spell.Accuracy;
            return firstChanceToHit || secondChanceToHit;
        }

        private static float CalculateDamageModifier(Character attacker, Character defender, bool? isPhysical = null) {
            var modifier = 1f;
            if (attacker.Persona.AttackBuff == StatsModifiers.Buff) {
                 modifier *= 1.3f;
            }  
            else if (attacker.Persona.AttackBuff == StatsModifiers.Debuff) {
                modifier /= 1.3f;
            } 
            
            if (defender.Persona.DefenceBuff == StatsModifiers.Buff) {
                modifier /= 1.3f;
            }
            else if (defender.Persona.DefenceBuff == StatsModifiers.Debuff) {
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

        private static float AttackVariance(int luck) {
            var random = Random.Range(0.95f, 1.06f);
            if (Random.value * 100 > luck) return random;

            var newRandom = Random.Range(0.95f, 1.06f);
            random = Mathf.Max(random, newRandom);
            return random;
        }

        private static void ResolveResistances(Character attacker, Character defender, Elements element, int damage) {
            switch (defender.Persona.Resistances[element])
            {
                case ResistanceModifiers.Resist:
                    defender.CurrentHealth -= Mathf.RoundToInt(damage * 0.5f);
                    break;
                case ResistanceModifiers.Weak:
                    defender.CurrentHealth -= Mathf.RoundToInt(damage * 1.5f);
                    break;
                case ResistanceModifiers.Reflect:
                    attacker.CurrentHealth -= damage;
                    break;
                case ResistanceModifiers.Absorb:
                    defender.CurrentHealth += damage;
                    break;
                case ResistanceModifiers.None:
                    defender.CurrentHealth -= damage;
                    break;
                case ResistanceModifiers.Block:
                    break;
            }
        }
    }
}