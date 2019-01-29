using System.Collections.Generic;
using Assets.Spells;
using Assets.Spells.SpellLexicon;
using Assets.Take_II.Scripts.Enums;

namespace Assets.Personas {
    public class TestPersona : PersonaBase
    {
        public override string Name => "TestPersona";
        public TestPersona() : base(Arcana.Fool, Elements.Almighty) {
            Level = 1;
         }
        protected override void SetBaseStats()
        {
            Stats = new Dictionary<Statistics, int> {
                {Statistics.Strength, 30}, 
                {Statistics.Magic, 30},
                {Statistics.Endurance, 30}, 
                {Statistics.Agility, 30}, 
                {Statistics.Luck, 30}
            };       
        }

         protected override void SetResistances()
        {
            Resistances[Elements.Fire] = ResistanceModifiers.Resist;
            Resistances[Elements.Ice] = ResistanceModifiers.Absorb;
            Resistances[Elements.Wind] = ResistanceModifiers.Weak;
            Resistances[Elements.Elec] = ResistanceModifiers.Block;
        }

        protected override List<SpellBase> GetBaseSpellbook()
        {
            return new List<SpellBase> {
                SpellLexicon.Fire.Agilao.Value,
                SpellLexicon.Wind.Magarula.Value,
                SpellLexicon.Elec.Zionga.Value, 
                SpellLexicon.Ice.Mabufula.Value
            };
        }

        protected override Dictionary<int, SpellBase> GetLockedSpells()
        {
            return new Dictionary<int, SpellBase> {
                
            };
        }
    }
}