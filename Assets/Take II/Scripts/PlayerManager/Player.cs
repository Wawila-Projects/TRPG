using System.Collections.Generic;
using Assets.Take_II.Scripts.Enums;
using Assets.Take_II.Scripts.HexGrid;
using Assets.Take_II.Scripts.Talent_Structures;
using UnityEngine;
using Random = System.Random;

namespace Assets.Take_II.Scripts.PlayerManager
{
    public class Player : MonoBehaviour
    {
        public Tile Location;
        public StatManager Stats;
        public int CurrentHealth;
        //public Equipment Equipment;
        public TalentTree TalentTree;
        public ResistanceManager Resistances;


        public bool IsDead;
        public bool IsEnemy;
        public bool IsHealer;
        public int WeaponRange; // { get { return Equipment.isRange } } 

        [SerializeField] internal List<string> StatsKeys;
        [SerializeField] internal List<int> StatsValues;

        void Awake()
        {
            StatsKeys = new List<string>();
            StatsValues = new List<int>();
            var rand = new Random();

            var stats = new Dictionary<Statistics, int>
            {
                {Statistics.Hp, rand.Next(9, 15)+1},
                {Statistics.Str, rand.Next(1, 7)+1},
                {Statistics.Mag, rand.Next(0, 5)+1},
                {Statistics.Skl, rand.Next(1, 7)+1},
                {Statistics.Spd, rand.Next(0, 2)+1},
                {Statistics.Def, rand.Next(0, 2)+1},
                {Statistics.Res, rand.Next(0, 5)+1},
                {Statistics.Luck, rand.Next(0, 3)+1}

            };

            Stats = new StatManager(stats);

            foreach (var stat in stats)
            {
                StatsKeys.Add(stat.Key.ToString());
                StatsValues.Add(stat.Value);
            }
            
            WeaponRange = 1;
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
            var damage = PhysicalDamage(target, isCritical);

            target.CurrentHealth -= damage;
        }

        public int PhysicalDamage(Player target, bool criticalHit)
        {
            var attackPower = Stats.PhysicalAttack();
            var tarDefencePower = target.Stats.PhysicalDefence();

            var damage = criticalHit ? 
                Stats.CriticalDamge(attackPower, tarDefencePower) : 
                Stats.Damage(attackPower, tarDefencePower);

            return damage;
        }

        public int MagicalDamage(Player target, bool criticalHit)
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

        public static Player ClonePlayer(this Player p)
        {
            var temp = UnityEngine.Object.Instantiate(p);
            var player = temp.GetComponent<Player>();
            UnityEngine.Object.Destroy(temp.gameObject);
            return player;
        }
    }
}