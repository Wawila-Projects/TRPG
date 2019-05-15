using System.Collections.Generic;
using Assets.Spells;
using Assets.Spells.SpellLexicon;
using Assets.Take_II.Scripts.Enums;

namespace Assets.Personas.Fool {
    public class Izanagi : PersonaBase
    {
        public override string Name => "Izanagi";
        public override bool IsPlayerPersona => true;

        public override Arcana Arcana => Arcana.Fool;
        public override Elements InheritanceElement => Elements.None;

        protected override void Awake() {
            Level = 1;
            base.Awake();
        }
        protected override void SetBaseStats()
        {   
            Stats = new Dictionary<Statistics, int> {
                {Statistics.Strength,   2}, 
                {Statistics.Magic,      2},
                {Statistics.Endurance,  2}, 
                {Statistics.Agility,    3}, 
                {Statistics.Luck,       1}
            };       
        }

        protected override void SetResistances()
        {
            Resistances[Elements.Curse] = ResistanceModifiers.Resist;
            Resistances[Elements.Ice] = ResistanceModifiers.Weak;
            Resistances[Elements.Bless] = ResistanceModifiers.Weak;
        }

        protected override List<SpellBase> GetBaseSpellbook()
        {
            return new List<SpellBase> {
                SpellLexicon.Elec.Zio, 
                SpellLexicon.Physical.Cleave, 

            };
        }

        protected override Dictionary<int, SpellBase> GetLockedSpells()
        {
            return new Dictionary<int, SpellBase> {
                
            };
        }
    }
}