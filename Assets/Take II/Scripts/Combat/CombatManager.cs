using System;
using System.Collections.Generic;
using Assets.Take_II.Scripts.InputManger;
using Assets.Take_II.Scripts.PlayerManager;
using UnityEngine;
using Random = System.Random;

namespace Assets.Take_II.Scripts.Combat
{
    public sealed class /*BasicAttack*/CombatManager : MonoBehaviour
    {
        public Player Attacker { get; set; }
        public Player Defender { get; set; }
        
        [SerializeField]
        private int _seed;
        private PlayerInteractions _interactions;
        private Random _random;

        void Awake()
        {
            _seed = (int)DateTime.Now.Ticks;
            _random = new Random(_seed);
            _interactions = gameObject.GetComponent<PlayerInteractions>();
        }

        void Update()
        {
            Attacker = _interactions.Selected;
            var target = _interactions.Target;
            Defender = target == null ? null : target.GetComponent<Player>();

            Combat();
        }
        
        
        public bool Combat()
        {
            if (Attacker == null || Defender == null)
                return false;

            if(!Attacker.IsInRange(Defender))
                return false;

            if (_interactions.IsInCombat)
                Attack();
            else if(_interactions.IsHealing)
                Heal();
                
            return true;
        }

        public IDictionary<string, double> GetCombatStats()
        {
            var combatStats = new Dictionary<string, double>
            {
                {"hp", 0},
                {"newhp", 0},
                {"hit", 0},
                {"att", 0},
                {"crit", 0},
                {"isHealing", 0 }
            };

            //TODO esta condicion esta mala
            if (_interactions.IsHealing)
            {
                var finalHealth = Defender.CurrentHealth + Attacker.Stats.Heal();
                Defender.CurrentHealth = finalHealth > Defender.Stats.Hp ? Defender.Stats.Hp : finalHealth;

                combatStats["hp"] = Defender.CurrentHealth;
                combatStats["newhp"] = finalHealth;
                combatStats["isHealing"] = 1;

                return combatStats;
            }

            var tarEvade = Defender.Stats.Evade();
            var hitRate = Attacker.Stats.HitRate();

            var accuracy = Attacker.Stats.Accuracy(hitRate, tarEvade);
            
            var tarCritEvade = Defender.Stats.CriticalEvade();
            var critRate = Attacker.Stats.CriticalRate();

            var critChance = Attacker.Stats.CriticalChance(critRate, tarCritEvade);

            var damage = Attacker.IsPhysical ? PhysicalDamage() : MagicalDamage();
            var resistedDamage = Defender.Resistances.ResistedDamage(damage);
            var resultingHealth = Defender.CurrentHealth - resistedDamage;

            combatStats["hp"] = Defender.CurrentHealth;
            combatStats["newhp"] = resultingHealth;
            combatStats["hit"] = accuracy;
            combatStats["crit"] = critChance;
            combatStats["att"] = resistedDamage;
            combatStats["damage"] = damage;
            combatStats["critDamage"] = damage * 3;
            combatStats["isHealing"] = 0;

            return combatStats;
        }

        private void Attack()
        {
            var combatStats = GetCombatStats();

            if(combatStats["hit"] < _random.Next(0, 100))
                return;

            var finalDamage = (int) (combatStats["crit"] >= _random.Next(0, 100) ? 
                                combatStats["critDamage"] : combatStats["damage"]);

            finalDamage = Defender.Resistances.ResistedDamage(finalDamage);

            Defender.CurrentHealth -= finalDamage;

        }

        private void Heal()
        {
            var combatStats = GetCombatStats();

            if (Defender.IsDead || (int)combatStats["isHealing"] == 1) 
                return;

            Defender.CurrentHealth = (int) combatStats["newhp"];
        }


        private int PhysicalDamage()
        {
            var attackPower = Attacker.Stats.PhysicalAttack();
            var tarDefencePower = Defender.Stats.PhysicalDefence();

            var damage = Attacker.Stats.Damage(attackPower, tarDefencePower);

            return damage;
        }

        private int MagicalDamage()
        {
            var attackPower = Attacker.Stats.MagicalAttack();
            var tarDefencePower = Defender.Stats.MagicalDefence();

            var damage = Attacker.Stats.Damage(attackPower, tarDefencePower);

            return damage;
        }
    }
}

