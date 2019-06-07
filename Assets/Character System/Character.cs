using System.Linq;
using UnityEngine;
using Assets.Personas;
using Asstes.CharacterSystem;

namespace Assets.CharacterSystem
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

        public StatusEffectController StatusEffect;

        [SerializeField]
        protected int _currentHP;
        public int CurrentHP
        {
            get { return _currentHP; }
            set
            {
                if (value > 0)
                {
                    _currentHP = value;
                    return;
                }
                _currentHP = 0;
                IsDead = true;
            }
        }

        [SerializeField]
        protected int _currentSP;
        public int CurrentSP
        {
            get { return _currentSP; }
            set
            {
                if (value > 0)
                {
                    _currentSP = value >= Sp ? Sp: value;
                    return;
                }
                _currentSP = 0;
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
            Level = Persona.Level;
            Name = gameObject.name;
            Hp = 100;
            Sp = 75;
            Movement = 3;
            CurrentMovement = Movement;

            CurrentHP = Hp;
            
            IsRange = false;
            WeaponRange = IsRange ? 2 : 1;
            OnAwake();
        }

        void Update() {
            OnUpdate();
        }
        
       public void LevelUp() {
            var changes = Persona.LevelUp();
            Level = changes.newLevel;
            
            var lostHp = Hp - CurrentHP;
            var lostSp = Sp - CurrentSP;

            Hp = Mathf.RoundToInt(Hp * 1.1f);
            Sp = Mathf.RoundToInt(Sp * 1.1f);

            CurrentHP = Hp - lostHp;
            CurrentSP = Sp - lostSp;
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
            var isInRange = distance <= CurrentMovement + WeaponRange;
            
             return isInRange;
        }

        public bool IsInRange(Tile tile)
        {
            if (tile == null)
                return false;

            var distance = Location.GetDistance(tile);
            var isInRange = distance <= CurrentMovement + WeaponRange;
            
             return isInRange;
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

        // TODO: Solve case when all Tiles at totalSteps is occupied 
        public Tile MoveTowards(Character other, int steps) 
        {
            var totalSteps = steps > CurrentMovement ? CurrentMovement : steps;
            var paths = AStar.FindPaths(Location, other.Location);

            foreach (var path in paths) 
            {
                 path.Remove(Location);
                if (path.Count > steps) 
                {
                    while (path.Count != totalSteps) 
                        path.Remove(path.Last());
                }
                var tile = path.Last();
                if (tile.Occupant != null)
                    continue;
                
                CurrentMovement -= totalSteps;
                return tile;
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