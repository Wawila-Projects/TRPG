using System.Collections.Generic;
using Assets.Spells;
using Assets.Take_II.Scripts.Combat;
using Assets.Take_II.Scripts.Enums;
using Assets.Take_II.Scripts.PlayerManager;

namespace Assets.Personas
{
    public abstract class PersonaBase {
        public abstract string Name { get; }
        public int Level { get; protected set; }
        public Arcana Arcana { get; protected set; }
        public SpellBook SpellBook { get;  protected set; }
        public Stats Stats { get;  protected set; }
        public Dictionary<Elements, ResistanceModifiers> Resistances { get; protected set; }
        public bool IsPlayerPersona = false;

        public PersonaBase(Arcana arcana, Elements inheritanceElement) {
            Arcana = arcana;
            SpellBook = new SpellBook(this, inheritanceElement, GetBaseSpellbook());
            Stats = GetBaseStats();

            Resistances = new Dictionary<Elements, ResistanceModifiers>
            {
                {Elements.Almighty, ResistanceModifiers.None },
                {Elements.Bless, ResistanceModifiers.None },
                {Elements.Curse, ResistanceModifiers.None },
                {Elements.Physical, ResistanceModifiers.None },
                {Elements.Elec, ResistanceModifiers.None },
                {Elements.Fire, ResistanceModifiers.None },
                {Elements.Ice, ResistanceModifiers.None },
                {Elements.Wind, ResistanceModifiers.None }
            };
        }

        protected abstract List<SpellBase> GetBaseSpellbook(); 
        protected abstract Dictionary<int, SpellBase> GetLockedSpells();
        protected abstract Stats GetBaseStats();
    }     
}