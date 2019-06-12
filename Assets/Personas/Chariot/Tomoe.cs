using System.Collections.Generic;
using Assets.Enums;
using Assets.Spells;
using Assets.Spells.SpellLexicon;

namespace Assets.Personas.Chariot
{
    public class Tomoe : PersonaBase
    {
        public override string Name => "Tomoe";
        public override bool IsPlayerPersona => true;

        public override Arcana Arcana => Arcana.Chariot;
        public override Elements InheritanceElement => Elements.Almighty;

        protected override void Awake() {
            Level = 1;
            base.Awake();
        }
        protected override IDictionary<Statistics, int> GetBaseStats()
        {   
            return new Dictionary<Statistics, int> {
                {Statistics.Strength,  7}, 
                {Statistics.Magic,     9},
                {Statistics.Endurance, 6}, 
                {Statistics.Agility,   7}, 
                {Statistics.Luck,      8}
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
                SpellLexicon.Ice.Bufu,
                SpellLexicon.Physical.Skewer, 
            };
        }

        protected override Dictionary<int, SpellBase> GetLockedSpells()
        {
            return new Dictionary<int, SpellBase> {
                {8, SpellLexicon.Ice.Mabufu},
                {15, SpellLexicon.Physical.SonicPunch},
            };
        }
    }
}