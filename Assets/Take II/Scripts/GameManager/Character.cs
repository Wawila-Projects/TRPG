using System.Linq;
using UnityEngine;
using Assets.Personas;

namespace Assets.Take_II.Scripts.GameManager
{
    public abstract class Character : MonoBehaviour
    {
        public string Name;
        public Tile Location;
        public int Movement { get; protected set; }
        public int CurrentMovement;
        public int Level;
        public int Hp;
        public int Sp;
        public PersonaBase Persona;

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
                    return;
                }
                _currentHealth = 0;
                IsDead = true;
            }
        }

        [SerializeField]
        protected int _currentSpiritPoints;
        public int CurrentSpiritPoints
        {
            get { return _currentSpiritPoints; }
            set
            {
                if (value > 0)
                {
                    _currentSpiritPoints = value >= Sp ? Sp: value;
                    return;
                }
                _currentSpiritPoints = 0;
            }
        }

        public bool IsDead;
        public bool IsRange;
        public int WeaponRange;
        public bool TurnFinished;
        public bool IsSurrounded;

        protected virtual void OnAwake() { }
        protected virtual void OnUpdate() { }
        
        void Awake() {
            Level = 1;
            Name = gameObject.name;
            Hp = 100;
            Sp = 75;
            Movement = 3;
            CurrentMovement = Movement;
            // Persona = new TestPersona();

            CurrentHealth = Hp;
            
            IsRange = false;
            WeaponRange = IsRange ? 2 : 1;
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
            var sameLocation = Location == other.Location;
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

            var distance = Location.GetDistance(other.Location);
            var isInRange = distance <= Movement + WeaponRange;
            
             return isInRange;
        }

        public bool IsInCombatRange(Character other) {
            var isNeighbor = Location.Neighbors.Contains(other.Location); 
            if (IsRange) {
                return !isNeighbor;
            }
            return isNeighbor;
        }   

        public int DistanceFromCombatRange(Character other)
        {
            var distance = Location.GetDistance(other.Location);
            return (int)(distance - WeaponRange);
        }

         public Tile MoveAway(Character other) 
         { 
             if (Movement == 0) {
                 return Location;
             }

            var possibleTiles = Location.Neighbors.Except(other.Location.Neighbors);
            foreach (var tile in possibleTiles) 
            {
                if (tile.Occupant != null) continue;
                CurrentMovement -= 1;
                return tile;
            }
            return null;
        }

        public Tile MoveTowards(Character other, int steps) 
        {
            var totalSteps = steps > CurrentMovement ? CurrentMovement : steps;
            var path = AStar.FindPath(Location, other.Location);
            path.Remove(Location);
            if (path.Count > steps) 
            {
                while (path.Count != totalSteps) 
                    path.Remove(path.Last());
            }
            var tile = path.Last();
            if (tile.Occupant == null)
            {
                CurrentMovement -= totalSteps;
                return tile;
            }
            tile = MoveTowardsIfOccupied(tile, other);
            return tile ?? Location;
        }

        private Tile MoveTowardsIfOccupied(Tile tile, Character other) 
        {
            foreach (var neighbor in tile.Neighbors) 
            {
                var optPath = AStar.FindPath(Location, neighbor);
                optPath.Remove(Location);
                
                var isReachable = optPath.Count <= Movement; 
                var isInRange = TileDistanceFromCombat() == 0;
                var isNotOccupied = !neighbor.IsOccupied;

                if (!isReachable || !isInRange || !isNotOccupied) continue;
                CurrentMovement -= optPath.Count;
                return neighbor;
            }
            return null;

            int TileDistanceFromCombat() {
                var distance = tile.GetDistance(other.Location);
                return (int)(distance - WeaponRange);
            }
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