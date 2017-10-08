using System.Collections.Generic;
using Assets.Take_II.Scripts.Combat;
using Assets.Take_II.Scripts.HexGrid;
using UnityEngine;
using Random = System.Random;

namespace Assets.Take_II.Scripts.PlayerManager
{
    public class Player : MonoBehaviour
    {
        public Tile Location;
        public StatManager Stats;
        public int CurrentHealth;

        public bool IsDead;
        public bool IsEnemy;
        public bool IsHealer;

        [SerializeField] internal List<string> StatsKeys;
        [SerializeField] internal List<int> StatsValues;

        void Awake()
        {
            StatsKeys = new List<string>();
            StatsValues = new List<int>();
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

                StatsKeys.Add(stat.Key);
                StatsValues.Add(stat.Value);
            }

            CurrentHealth = Stats.Hp;
        }


        void Update()
        {
            if(CurrentHealth > 0)
                return;

            CurrentHealth = 0;
            IsDead = true;

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

    public static class PlayerUtils
    {
        public static bool IsEqualTo(this Player p, Player other)
        {
            var leftNull = p == null;
            var rightNull = other == null;

            if (leftNull || rightNull)
                return false;

            var sameName = p.name == other.name;
            var sameLocation = p.Location.IsEqualTo(other.Location);

            return sameName && sameLocation;
        }
    }
}