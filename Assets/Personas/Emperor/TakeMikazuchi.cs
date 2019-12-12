using System.Collections.Generic;
using Assets.Spells;
using Assets.Spells.SpellLexicon;
using Assets.Enums;

namespace Assets.Personas.Emperor {
    public class TakeMikazuchi : PersonaBase
    {
        public override string Name => "Take-Mikazuchi";
        public override bool IsPlayerPersona => true;

        public override Arcana Arcana => Arcana.Emperor;
        public override Elements InheritanceElement => Elements.Almighty;

        protected override void Awake() {
            Level = 1;
            base.Awake();
        }
        protected override IDictionary<Statistics, int> GetBaseStats()
        {   
            return new Dictionary<Statistics, int> {
                {Statistics.Strength,   9}, 
                {Statistics.Magic,      5},
                {Statistics.Endurance,  9}, 
                {Statistics.Agility,    4}, 
                {Statistics.Luck,       5}
            };       
        }

        protected override void SetResistances()
        {
            Resistances[Elements.Elec] = ResistanceModifiers.Resist;
            Resistances[Elements.Wind] = ResistanceModifiers.Weak;
        }

        protected override List<ISpell> GetBaseSpellbook()
        {
            return new List<ISpell> {
                SpellLexicon.Physical.Cleave, 
                SpellLexicon.Elec.Zio, 
            };
        }

        protected override Dictionary<int, ISpell> GetLockedSpells()
        {
            return new Dictionary<int, ISpell> {
                {8, SpellLexicon.Physical.SingleShot},
                {15, SpellLexicon.Elec.Mazio},
            };
        }
    }
}