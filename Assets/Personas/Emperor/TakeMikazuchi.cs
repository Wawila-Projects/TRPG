using System.Collections.Generic;
using Assets.Spells;
using Assets.Spells.SpellLexicon;
using Assets.Take_II.Scripts.Enums;

namespace Assets.Personas.Emperor {
    public class TakeMikazuchi : PersonaBase
    {
        public override string Name => "Take-Mikazuchi";
        public override bool IsPlayerPersona => true;

        public override Arcana Arcana => Arcana.Emperor;
        public override Elements InheritanceElement => Elements.None;

        protected override void Awake() {
            Level = 25;
            base.Awake();
        }
        protected override void SetBaseStats()
        {   
            Stats = new Dictionary<Statistics, int> {
                {Statistics.Strength,   20}, 
                {Statistics.Magic,      11},
                {Statistics.Endurance,  17}, 
                {Statistics.Agility,    15}, 
                {Statistics.Luck,       14}
            };       
        }

        protected override void SetResistances()
        {
            Resistances[Elements.Elec] = ResistanceModifiers.Resist;
            Resistances[Elements.Wind] = ResistanceModifiers.Weak;
        }

        protected override List<SpellBase> GetBaseSpellbook()
        {
            return new List<SpellBase> {
                SpellLexicon.Elec.Mazio, 
                SpellLexicon.Elec.Zionga, 
            };
        }

        protected override Dictionary<int, SpellBase> GetLockedSpells()
        {
            return new Dictionary<int, SpellBase> {
                {40, SpellLexicon.Elec.Mazionga},
                {54, SpellLexicon.Elec.Ziodyne},
            };
        }
    }
}