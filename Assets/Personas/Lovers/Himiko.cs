
using System.Collections.Generic;
using Assets.Spells;
using Assets.Spells.SpellLexicon;
using Assets.Take_II.Scripts.Enums;

namespace Assets.Personas.Lovers {
    public class Himiko : PersonaBase
    {
        public override string Name => "Himiko";
        public override bool IsPlayerPersona => true;
        public override Arcana Arcana => Arcana.Lovers;
        public override Elements InheritanceElement => Elements.None;

        protected override void Awake() {
            Level = 34;
            base.Awake();
        }
        protected override void SetBaseStats()
        {   
            Stats = new Dictionary<Statistics, int> {
                {Statistics.Strength,   15}, 
                {Statistics.Magic,      25},
                {Statistics.Endurance,  20}, 
                {Statistics.Agility,    21}, 
                {Statistics.Luck,       24}
            };       
        }

        protected override void SetResistances()
        {
        }

        protected override List<SpellBase> GetBaseSpellbook()
        {
            return new List<SpellBase> {
            };
        }

        protected override Dictionary<int, SpellBase> GetLockedSpells()
        {
            return new Dictionary<int, SpellBase> {
            };
        }
    }
}