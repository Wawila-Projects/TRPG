using System.Collections.Generic;
using Assets.Spells;
using Assets.Take_II.Scripts.Enums;

namespace Assets.Personas
{
    public abstract class PersonaBase {
        public abstract string Name { get; }
        public int Level { get; protected set; }
        public Arcana Arcana { get; protected set; }
        public int Strength { get; protected set; }
        public int Magic { get; protected set; }
        public int Endurance { get; protected set; }
        public int Agility { get; protected set; }
        public int Luck { get; protected set; }
        public StatsModifiers AttackBuff = StatsModifiers.None;
        public StatsModifiers DefenceBuff = StatsModifiers.None;
        public StatsModifiers EvadeBuff = StatsModifiers.None;
        public StatsModifiers HitBuff = StatsModifiers.None;
        public bool MindCharged = false;
        public bool PowerCharged = false;

        // TODO: Add buffs for Elemental Attacks; Amp, Boost and accesories
        public SpellBook SpellBook { get; protected set; }
        public Dictionary<Elements, ResistanceModifiers> Resistances { get; protected set; }
        public bool IsPlayerPersona = false;

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

        protected abstract void SetBaseStats();
        protected abstract void SetResistances();
        protected abstract List<SpellBase> GetBaseSpellbook(); 
        protected abstract Dictionary<int, SpellBase> GetLockedSpells();
    }     
}