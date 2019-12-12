
using System.Collections.Generic;
using Assets.Spells;
using Assets.Spells.SpellLexicon;
using Assets.Enums;

namespace Assets.Personas.Lovers {
    public class Himiko : PersonaBase
    {
        public override string Name => "Himiko";
        public override bool IsPlayerPersona => true;
        public override Arcana Arcana => Arcana.Lovers;
        public override Elements InheritanceElement => Elements.Almighty;

        protected override void Awake() {
            Level = 34;
            base.Awake();
        }
        protected override IDictionary<Statistics, int> GetBaseStats()
        {   
            return new Dictionary<Statistics, int> {
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

        protected override List<ISpell> GetBaseSpellbook()
        {
            return new List<ISpell> {
            };
        }

        protected override Dictionary<int, ISpell> GetLockedSpells()
        {
            return new Dictionary<int, ISpell> {
            };
        }
    }
}