using System.Linq;
using UnityEngine;
using Assets.Personas;
using Asstes.CharacterSystem.StatusEffects;
using Assets.CharacterSystem.PassiveSkills;

namespace Assets.CharacterSystem
{

    [RequireComponent(typeof(PersonaBase))]
    [RequireComponent(typeof(StatusEffectController))]
    [RequireComponent(typeof(PassiveSkillController))]
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
        public PassiveSkillController PassiveSkills;

        [SerializeField]
        protected int _currentHP;

        [SerializeField]
        protected int _currentSP;

        public bool IsDead;
        public bool IsRange;
        public int WeaponRange;
        public bool TurnFinished;
        public bool IsSurrounded;
        public (bool isActive, int count) OneMore;

        public int CurrentHP
        {
            get => _currentHP;
            set
            {
                if (value > 0)
                {
                    _currentHP = value >= Hp ? Hp: value;
                    return;
                }
                _currentHP = 0;
                IsDead = true;
                Die();
            }
        }

         public int CurrentSP
        {
            get => _currentSP;
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

        protected virtual void OnAwake() { }
        protected virtual void OnUpdate() { }
        public virtual void Die () {
            StatusEffect.ClearStatusEffect();
            PassiveSkills.HandleStartSkills(false);
            PassiveSkills.HandleTurnSkills(false);
            PassiveSkills.HandleEndSkills(false);
         }
        
        void Awake() {
            if (StatusEffect == null) {
                StatusEffect = GetComponent<StatusEffectController> ();
            }

            if (PassiveSkills == null) {
                PassiveSkills = GetComponent<PassiveSkillController>();
            }

            Level = Persona.Level;
            Name = gameObject.name;
            Movement = 3;
            CurrentMovement = Movement;

            CurrentHP = Hp;
            CurrentSP = Sp;

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
            Hp += 4;
            Sp += 3;

            CurrentHP += 4;
            CurrentSP += 3;

            if (changes.spell is PassiveSkillsBase passiveSkill) {
                PassiveSkills.AddSkill(passiveSkill);
            }
        }

        public void AddOneMore() {
            OneMore.isActive = true;
            OneMore.count++;

            CurrentMovement++;
            TurnFinished = false;

            Debug.Log ($"{Name} One More!");
        }

        public void DeactivateOneMore() {
            OneMore.isActive = false;
        }

        public void ResetOneMore() {
            OneMore = (false, 0);
        }

        public Character ClonePlayer()
        {
            return GetComponent<Character>().gameObject.GetComponent<Character>();
        }

        public bool IsInMeleeRange(Character other) {
            return Location.Neighbors.Any( t => t == other.Location);
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
            return (distance - WeaponRange);
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
         try {
            var distance = DistanceFromCombatRange(other);
            if (distance > 0 )
                return MoveTowards(other, distance);
            if (distance < 0) 
                return MoveAway(other);
            return null;
         } catch (System.InvalidOperationException) {
             return null;
         }
        }
    }
}