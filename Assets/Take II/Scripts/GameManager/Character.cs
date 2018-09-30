using System.Linq;
using Assets.Take_II.Scripts.HexGrid;
using Assets.Take_II.Scripts.PlayerManager;
using UnityEngine;

namespace Assets.Take_II.Scripts.GameManager
{
    public class Character : MonoBehaviour
    {
        public string Name;
        public Tile Location;

        [SerializeField]
        protected int _currentHealth;
        public int CurrentHealth
        {
            get { return _currentHealth; }
            set
            {
                if (value > 0)
                {
                    _currentHealth = value;
                }
                else
                {
                    _currentHealth = 0;
                    IsDead = true;
                }
            }
        }
        
        public Stats Stats;
        public int Movement;
        public bool IsDead;
        public bool IsRange;
        public int WeaponRange;
        public bool TurnFinished;

        public virtual void OnAwake() { }
        public virtual void OnUpdate() { }
        
        void Awake() {
            Name = gameObject.name;
            Stats = new Stats {
                Hp = 100,
                Movement = 3,
                Strength = 10,
                Magic = 10,
                Endurance = 5,
                Luck = 7,
                Agility = 6
            };
            
            WeaponRange = IsRange ? 2 : 1;
            CurrentHealth = Stats.Hp;
            Movement = Stats.Movement;
            OnAwake();
        }

        void Update() {
            OnUpdate();
        }

        public bool IsEqualTo(Character other)
        {
            if (other == null)
                return false;

            var sameName = name == other.name;
            var sameLocation = Location.IsEqualTo(other.Location);

            return sameName && sameLocation;
        }

        public Character ClonePlayer()
        {
            return GetComponent<Character>().gameObject.GetComponent<Character>();
        }

        public bool IsInRange(Character other)
        {
            if (other == null)
                return false;

            var distance = Location.Distance(other.Location);

            return distance <= Stats.Movement + WeaponRange;
        }

        public int DistanceFromCombatRange(Character other)
        {
            var distance = Location.Distance(other.Location);
            return (int)(distance - WeaponRange);
        }

         public Tile MoveAway(Character other) 
         { 
            var possibleTiles = Location.Neighbors.Except(other.Location.Neighbors);
            foreach (var tile in possibleTiles) 
            {
                if (tile.OccupiedBy != null) continue;
                Movement -= 1;
                return tile;
            }
            return null;
        }

        public Tile MoveTowards(Character other, int steps) 
        {
            var totalSteps = steps > Movement ? Movement : steps;
            var pathfinder = new AStar();
            var path = pathfinder.FindPath(Location, other.Location);
            path.Remove(Location);
            if (path.Count > steps) 
            {
                while (path.Count != totalSteps) 
                    path.Remove(path.Last());
            }
            var tile = path.Last();
            if (tile.OccupiedBy == null)
            {
                Movement -= totalSteps;
                return tile;
            }
            return MoveTowardsIfOccupied(tile, other, pathfinder);
        }

        private Tile MoveTowardsIfOccupied(Tile tile, Character other, AStar pathfinder) 
        {
            foreach (var neighbor in tile.Neighbors) 
            {
                var optPath = pathfinder.FindPath(Location, neighbor);
                optPath.Remove(Location);
                
                var isReachable = optPath.Count <= Movement; 
                var isInRange = neighbor.DistanceFromCombatRange(this, other) == 0;
                var isNotOccupied = neighbor.OccupiedBy == null;

                if (!isReachable || !isInRange || !isNotOccupied) continue;
                Movement -= optPath.Count;
                return neighbor;
            }
            return null;
        }

        public Tile MoveToRange(Character other) {
            var distance = DistanceFromCombatRange(other);
            if (distance > 0 )
                return MoveTowards(other, distance);
            if (distance < 0) 
                return MoveAway(other);
            return null;
        }
    }
}