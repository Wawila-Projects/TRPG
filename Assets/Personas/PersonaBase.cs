using System;
using System.Collections.Generic;
using Assets.Spells;
using Assets.Take_II.Scripts.Enums;

namespace Assets.Personas
{
    public abstract class PersonaBase {
        public abstract string Name { get; }
        public int Level { get; protected set; }
        public Arcana Arcana { get; }
        public int Strength => Stats[Statistics.Strength] + StatBuffs.GetValueOrDefault(Statistics.Strength, 0);
        public int Magic => Stats[Statistics.Magic] + StatBuffs.GetValueOrDefault(Statistics.Magic, 0);
        public int Endurance => Stats[Statistics.Endurance] + StatBuffs.GetValueOrDefault(Statistics.Endurance, 0);
        public int Agility => Stats[Statistics.Agility] + StatBuffs.GetValueOrDefault(Statistics.Agility, 0);
        public int Luck => Stats[Statistics.Luck] + StatBuffs.GetValueOrDefault(Statistics.Luck, 0);
        public StatsModifiers AttackBuff = StatsModifiers.None;
        public StatsModifiers DefenceBuff = StatsModifiers.None;
        public StatsModifiers EvadeBuff = StatsModifiers.None;
        public StatsModifiers HitBuff = StatsModifiers.None;
        public bool MindCharged = false;
        public bool PowerCharged = false;
        public bool IsPlayerPersona = false;

        // TODO: Add buffs for Elemental Attacks; Amp, Boost and accesories
        public SpellBook SpellBook { get; protected set; }
        public Dictionary<Elements, ResistanceModifiers> Resistances { get; protected set; }

        protected Dictionary<Statistics, int> Stats;
        protected Dictionary<Statistics, int> StatBuffs;
    
        public PersonaBase(Arcana arcana, Elements inheritanceElement) {
            Arcana = arcana;
            SpellBook = new SpellBook(this, inheritanceElement, GetBaseSpellbook()); 

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

            SetResistances();
            SetBaseStats();
        }

        public int LevelUp(int statsUp = 3) {
            ++Level;
            
            var random = new Random(DateTime.Now.Millisecond);

            var statistics = (Statistics[]) Enum.GetValues(typeof(Statistics));
            var statsToLevel = new List<Statistics>();
            for(var i = 0; i < statsUp; ++i) {
                var stat = statistics[random.Next(statistics.Length)];
                statsToLevel.Add(stat);
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
        
        protected abstract void SetBaseStats();
        protected abstract void SetResistances();
        protected abstract List<SpellBase> GetBaseSpellbook(); 
        protected abstract Dictionary<int, SpellBase> GetLockedSpells();
    }     
}