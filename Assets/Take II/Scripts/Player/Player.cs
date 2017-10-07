using System.Collections.Generic;
using System.Security.Cryptography;
using Assets.Take_II.Scripts.Combat;
using Assets.Take_II.Scripts.HexGrid;
using UnityEngine;
using UnityEngine.Assertions.Must;
using Random = System.Random;

namespace Assets.Take_II.Scripts.Player
{
    
    public class Player : MonoBehaviour
    {
        public float PosX;
        public float Posy;
        public Tile Location;
        public int CurrentHealth;
        public bool IsDead;


        public bool att;
        public Player tar;

        public StatManager Stats;

        [SerializeField]
        private List<string> _keys;
        [SerializeField]
        private List<int> _values;

        void Update()
        {
            if (!att) return;

            Combat(tar);
            att = false;
        }

        void Awake()
        {
            _keys = new List<string>();
            _values = new List<int>();
            var rand = new Random();

            var stats = new Dictionary<string, int>
            {
                {"hp", rand.Next(9, 15)+1},
                {"str", rand.Next(1, 7)+1},
                {"mag", rand.Next(0, 5)+1},
                {"skl", rand.Next(1, 7)+1},
                {"spd", rand.Next(0, 2)+1},
                {"def", rand.Next(0, 2)+1},
                {"res", rand.Next(0, 5)+1},
                {"luck", rand.Next(0, 3)+1}

            };

            Stats = new StatManager(stats);

            foreach (var stat in stats)
            {

                _keys.Add(stat.Key);
                _values.Add(stat.Value);
            }

            CurrentHealth = Stats.Hp;
        }

        public void Combat(Player target)
        {
            var rand = new Random();

            var tarEvade = target.Stats.Evade();
            var hitRate = Stats.HitRate();

            var accuracy = Stats.Accuracy(hitRate, tarEvade);

            if (accuracy < rand.Next(0, 100) + 1)
                return;

            var tarCritEvade = target.Stats.CriticalEvade();
            var critRate = Stats.CriticalRate();

            var critChance = Stats.CriticalChance(critRate, tarCritEvade);

            var isCritical = critChance > rand.Next(0, 100) + 1;
            var damage = PhysicalCombat(target, isCritical);

            target.CurrentHealth -= damage;

            if (target.CurrentHealth >= 0) return;

            target.CurrentHealth = 0;
            target.IsDead = true;
        }

        public int PhysicalCombat(Player target, bool criticalHit)
        {
            var attackPower = Stats.PhysicalAttack();
            var tarDefencePower = target.Stats.PhysicalDefence();

            var damage = criticalHit ? 
                Stats.CriticalDamge(attackPower, tarDefencePower) : 
                Stats.Damage(attackPower, tarDefencePower);

            return damage;
        }

        public int MagicalCombat(Player target, bool criticalHit)
        {
            var attackPower = Stats.MagicalAttack();
            var tarDefencePower = target.Stats.MagicalDefence();

            var damage = criticalHit ?
                Stats.CriticalDamge(attackPower, tarDefencePower) :
                Stats.Damage(attackPower, tarDefencePower);

            return damage;
        }
    }
}