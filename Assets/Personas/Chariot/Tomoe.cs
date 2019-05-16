using System.Collections.Generic;
using Assets.Spells;
using Assets.Spells.SpellLexicon;
using Assets.Take_II.Scripts.Enums;

namespace Assets.Personas.Chariot {
    public class Tomoe : PersonaBase
    {
        public override string Name => "Tomoe";
        public override bool IsPlayerPersona => true;

        public override Arcana Arcana => Arcana.Chariot;
        public override Elements InheritanceElement => Elements.None;

        protected override void Awake() {
            Level = 4;
            base.Awake();
        }
        protected override void SetBaseStats()
        {   
            Stats = new Dictionary<Statistics, int> {
                {Statistics.Strength,  4}, 
                {Statistics.Magic,     4},
                {Statistics.Endurance, 4}, 
                {Statistics.Agility,   2}, 
                {Statistics.Luck,      3}
            };       
        }

        protected override void SetResistances()
        {
            Resistances[Elements.Ice] = ResistanceModifiers.Resist;
            Resistances[Elements.Fire] = ResistanceModifiers.Weak;
        }

        protected override List<SpellBase> GetBaseSpellbook()
        {
            return new List<SpellBase> {
                SpellLexicon.Physical.Skewer, 
            };
        }

        protected override Dictionary<int, SpellBase> GetLockedSpells()
        {
            return new Dictionary<int, SpellBase> {
                {6, SpellLexicon.Ice.Bufu},
            };
        }
    }
}