using System;
using System.Collections.Generic;
using Assets.Take_II.Scripts.Enums;
using Assets.Take_II.Scripts.HexGrid;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = System.Random;
using System.Linq;

namespace Assets.Take_II.Scripts.PlayerManager
{
    public class Player : MonoBehaviour
    {
        public string Name;
        public Tile Location;
        public int CurrentHealth;
        public int CurrentActionPoints;

        //public Equipment Equipment;
        public StatManager Stats;
        public TalentTree TalentTree;
        public ResistanceManager Resistances;
        public ActionHandler ActionsHandler;
        //public SkillBook SkillBook;
        //public EffectsHandler EffectsHandler;

        public int Movement;
        public int Seed;

        [SerializeField]
        private double _seed;

        public bool IsDead;
        public bool IsEnemy;
        public bool IsHealer;
        public bool IsRange;
        public bool IsPhysical; 
        public bool IsMagical;
        public int WeaponRange; // { get { return Equipment.isRange } } 

        [SerializeField] internal List<string> StatsKeys;
        [SerializeField] internal List<int> StatsValues;

        void Awake()
        {
            Resistances = new ResistanceManager();

            StatsKeys = new List<string>();
            StatsValues = new List<int>();
            //EffectsHandler = new EffectsHandler(Stats, SkillBook);
            //var seed = (int) (Math.Sin(gameObject.name.Last()) + Math.Tan(gameObject.name.First()));
            _seed = Math.Log(Seed) * 1000;
            var rand = new Random((int) _seed);
           
            Name = gameObject.name;

            var stats = new Dictionary<Statistics, int>
            {
                {Statistics.Hp, rand.Next(100, 200)},
                {Statistics.Str, rand.Next(1, 20)},
                {Statistics.Mag, rand.Next(1, 20)},
                {Statistics.Skl, rand.Next(1, 20)},
                {Statistics.Spd, rand.Next(1, 10)},
                {Statistics.Def, rand.Next(1, 10)},
                {Statistics.Res, rand.Next(1, 20)},
                {Statistics.Luck, rand.Next(1, 10)}

            };

            Stats = new StatManager(stats, Movement);

            foreach (var stat in stats)
            {
                StatsKeys.Add(stat.Key.ToString());
                StatsValues.Add(stat.Value);
            }
            
            WeaponRange = IsRange ? 2 : 1;
            CurrentHealth = Stats.Hp;
        }

        void Update()
        {
            if(CurrentHealth > 0)
                return;

            CurrentHealth = 0;
            IsDead = true;
        }

        public void EndTurn() {
            CurrentActionPoints = ActionsHandler.ActionPoints;
        }

         public bool IsEqualTo(Player other)
        {
            var leftNull = this == null;
            var rightNull = other == null;

            if (leftNull || rightNull)
                return false;

            var sameName = name == other.name;
            var sameLocation = Location.IsEqualTo(other.Location);

            return sameName && sameLocation;
        }

        public Player ClonePlayer()
        {
            var temp = Object.Instantiate(this);
            var player = temp.GetComponent<Player>();
            Object.Destroy(temp.gameObject);
            return player;
        }

        public bool IsInRange(Player other)
        {
            if (other == null)
                return false;

            var distance = Location.Distance(other.Location);

            return distance <= Stats.Movement + WeaponRange;
        }

        public bool IsInCombatRange(Player other) {
            if (other == null)
                return false;

            var distance = Location.Distance(other.Location);
            return distance <= WeaponRange;
        }

         public Tile MoveAway(Player other) { 
            var possibleTiles = Location.Neighbors.Except(other.Location.Neighbors);
            
            foreach (var tile in possibleTiles) {
                if (tile.OccupiedBy != null) 
                    continue;
                return tile;
            }
            return null;
        }
    }
}