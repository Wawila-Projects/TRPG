using System;
using System.Linq;
using System.Collections.Generic;
using Assets.Spells;
using Assets.Enums;
using Assets.Utils;
using Asstes.CharacterSystem.StatusEffects;

namespace Assets.Personas
{
    
    [Serializable]
    public abstract class PersonaBase: UnityEngine.MonoBehaviour {
        public abstract string Name { get; }
        public int Level { get; protected set; }
        public abstract Arcana Arcana { get; }
        public abstract Elements InheritanceElement { get; }
        public SpellBook SpellBook { get; protected set; }
        public IDictionary<Elements, ResistanceModifiers> Resistances { get; protected set; }
        public virtual bool IsPlayerPersona => false;
        protected IDictionary<Statistics, int> Stats;

        public int Strength => Stats[Statistics.Strength];
        public int Magic => Stats[Statistics.Magic];
        public int Endurance => Stats[Statistics.Endurance];
        public int Agility => Stats[Statistics.Agility];
        public int Luck => Stats[Statistics.Luck];

        public StatsModifiers AttackBuff = StatsModifiers.None;
        public StatsModifiers DefenceBuff = StatsModifiers.None;
        public StatsModifiers EvadeBuff = StatsModifiers.None;
        public StatsModifiers HitBuff = StatsModifiers.None;
        public float CounterChance = 0f;
        public bool MindCharged = false;
        public bool PowerCharged = false;
        public bool ArmsMaster = false;
        public bool SpellMaster = false;
        public bool AptPupil = false;
        public bool DivineGrace = false;
        public IDictionary<Elements, float> ElementDamageModifier;
        public IDictionary<StatusConditions, float> StatusConditionModifier;
        private IDictionary<Elements, ResistanceModifiers> OriginalResistances;
       

        // TODO: Add ability to buff/debuff resistances

        // protected Probability<Statistics> Probability;
    
        //** For Serializing */
        public double BST = 0;
        public List<string> StatsKeys;
        public List<int> StatsValues;
        public SpellBook _spellBook;
         //** End */

        public override string ToString() => $"{Level}| {Name} | {Arcana.ToString()}";

         protected virtual void Awake() {
            SpellBook = new SpellBook(this, InheritanceElement, GetBaseSpellbook(), GetLockedSpells());
            
            var elements = EnumUtils<Elements>.GetValues()
                .Where( w => w != Elements.None && w != Elements.Recovery );

            
            var resistances = elements.Select( s => new KeyValuePair<Elements, ResistanceModifiers>(s, ResistanceModifiers.None) );
            Resistances = resistances.ToDictionary();
            
            var modifiers = elements.Select( s => new KeyValuePair<Elements, float>(s, 1f) ); 
            ElementDamageModifier = modifiers.ToDictionary();
            
            StatusConditionModifier = EnumUtils<StatusConditions>.GetValues()
                .Where( w => w != StatusConditions.None && w != StatusConditions.Down )
                .Select( s => new KeyValuePair<StatusConditions, float> (s, 1f))
                .ToDictionary();

            SetResistances ();
            OriginalResistances = Resistances;
            Stats = GetBaseStats ();

            StatsKeys = Stats.Keys.Select((k) => k.ToString()).ToList();
            StatsValues = Stats.Values.ToList();
            _spellBook = SpellBook;
        }

        public (int newLevel, List<Statistics> statUps, ISpell spell) LevelUp(int statsUps = 3) {
            ++Level;
            var random = new Random(DateTime.Now.Millisecond);
            var statistics = EnumUtils<Statistics>.GetValues();
            var statsToLevel = new List<Statistics>();

            for(var i = 0; i < statsUps; ++i) {
                var stat = statistics[random.Next(statistics.Length)];
                if (Stats[stat] == 99) {
                    --i;
                    continue;
                }
                statsToLevel.Add(stat);
                ++Stats[stat];
            } 

            var (_, newSpell) = SpellBook.LevelUp();

            StatsValues = Stats.Values.ToList();
            BST = StatsValues.Average();
            
            return (Level, statsToLevel, newSpell);
        }

        public int LevelUp(List<Statistics> stats) {
            ++Level;
            foreach(var stat in stats) {
                if (Stats[stat] == 99)
                    continue;
                ++Stats[stat];
            }
            return Level;
        }

        public bool ChangeResistances(Elements element, ResistanceModifiers modifier = ResistanceModifiers.None, 
            bool clear = false, bool buff = false, bool debuff = false) {

            if (clear && buff && debuff) return false;

            if (clear) {
                Resistances[element] = OriginalResistances[element];
                return true;
            } 

            if (buff && modifier > Resistances[element]) {
                Resistances[element] = modifier;
                return true;
            }

            if (debuff && modifier < Resistances[element]) {
                Resistances[element] = modifier;
                return true;
            }
            
            return false;
        }

        protected abstract IDictionary<Statistics, int> GetBaseStats();
        protected abstract void SetResistances();
        protected abstract List<ISpell> GetBaseSpellbook(); 
        protected abstract Dictionary<int, ISpell> GetLockedSpells();
    }     
}