using System;
using System.Linq;
using System.Collections.Generic;
using Assets.Spells;
using Assets.Enums;
using Assets.Utils;
using Assets.ProbabilitySystem;

namespace Assets.Personas
{
    
    [Serializable]
    public abstract class PersonaBase: UnityEngine.MonoBehaviour {
        public abstract string Name { get; }
        public int Level { get; protected set; }
        public abstract Arcana Arcana { get; }
        public abstract Elements InheritanceElement { get; }
        public int Strength => Stats[Statistics.Strength] + StatBuffs.GetValueOrDefault(Statistics.Strength);
        public int Magic => Stats[Statistics.Magic] + StatBuffs.GetValueOrDefault(Statistics.Magic);
        public int Endurance => Stats[Statistics.Endurance] + StatBuffs.GetValueOrDefault(Statistics.Endurance);
        public int Agility => Stats[Statistics.Agility] + StatBuffs.GetValueOrDefault(Statistics.Agility);
        public int Luck => Stats[Statistics.Luck] + StatBuffs.GetValueOrDefault(Statistics.Luck);
        public StatsModifiers AttackBuff = StatsModifiers.None;
        public StatsModifiers DefenceBuff = StatsModifiers.None;
        public StatsModifiers EvadeBuff = StatsModifiers.None;
        public StatsModifiers HitBuff = StatsModifiers.None;
        public bool MindCharged = false;
        public bool PowerCharged = false;
        public virtual bool IsPlayerPersona => false;

        // TODO: Add buffs for Elemental Attacks; Amp, Boost and accesories
        public SpellBook SpellBook { get; protected set; }
        public IDictionary<Elements, ResistanceModifiers> Resistances { get; protected set; }
        protected IDictionary<Statistics, int> Stats;
        protected IDictionary<Statistics, int> StatBuffs;

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
            
            Resistances = new Dictionary<Elements, ResistanceModifiers>
            {
                {Elements.Almighty, ResistanceModifiers.None },
                {Elements.Bless, ResistanceModifiers.None },
                {Elements.Curse, ResistanceModifiers.None },
                {Elements.Physical, ResistanceModifiers.None },
                {Elements.Elec, ResistanceModifiers.None },
                {Elements.Fire, ResistanceModifiers.None },
                {Elements.Ice, ResistanceModifiers.None },
                {Elements.Wind, ResistanceModifiers.None },
                {Elements.Nuke, ResistanceModifiers.None}, 
                {Elements.Psy, ResistanceModifiers.None}
            };

            SetResistances ();
            Stats = GetBaseStats ();
            // Probability = new Probability<Statistics> (Stats);

            StatsKeys = Stats.Keys.Select((k) => k.ToString()).ToList();
            StatsValues = Stats.Values.ToList();
            _spellBook = SpellBook;
        }

        public (int newLevel, List<Statistics> statUps) LevelUp(int statsUps = 3) {
            ++Level;
            var random = new Random(DateTime.Now.Millisecond);
            // var statistics = (Statistics[]) Enum.GetValues(typeof(Statistics));
            var statistics = GetBaseStats ();
            var statsToLevel = new List<Statistics>();

            for(var i = 0; i < statsUps; ++i) {
                var stat = Probability<Statistics>.GetResult(statistics, random);
                if (Stats[stat] == 99) {
                    --i;
                    statistics.Remove(stat);
                    continue;
                }
                statsToLevel.Add(stat);
                ++Stats[stat];
            } 

            SpellBook.LevelUp();

            StatsValues = Stats.Values.ToList();
            BST = StatsValues.Average();
            
            return (Level, statsToLevel);
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

        public void BuffStats(Dictionary<Statistics, int> stats) {
            if(StatBuffs == null) {
                StatBuffs = new Dictionary<Statistics, int>();
            }

            foreach(var buff in stats) {
                StatBuffs[buff.Key] += buff.Value;
            }
        }

        protected abstract IDictionary<Statistics, int> GetBaseStats();
        protected abstract void SetResistances();
        protected abstract List<SpellBase> GetBaseSpellbook(); 
        protected abstract Dictionary<int, SpellBase> GetLockedSpells();
    }     
}