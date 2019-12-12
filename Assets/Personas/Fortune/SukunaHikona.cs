using System.Collections.Generic;
using Assets.Spells;
using Assets.Spells.SpellLexicon;
using Assets.Enums;

namespace Assets.Personas.Fortune {
    public class SukunaHikona : PersonaBase
    {
        public override string Name => "Sukuna-Hikona";
        public override bool IsPlayerPersona => true;

        public override Arcana Arcana => Arcana.Fortune;
        public override Elements InheritanceElement => Elements.Almighty;

        protected override void Awake() {
            Level = 1;
            base.Awake();
        }
        protected override IDictionary<Statistics, int> GetBaseStats()
        {   
            return new Dictionary<Statistics, int> {
                {Statistics.Strength,   5}, 
                {Statistics.Magic,      10},
                {Statistics.Endurance,  5}, 
                {Statistics.Agility,    7}, 
                {Statistics.Luck,       9}
            };       
        }

        protected override void SetResistances()
        {
            Resistances[Elements.Bless] = ResistanceModifiers.Resist;
            Resistances[Elements.Curse] = ResistanceModifiers.Resist;
        }

        protected override List<ISpell> GetBaseSpellbook()
        {
            return new List<ISpell> {
                SpellLexicon.Fire.Agi, 
                SpellLexicon.Wind.Garu,
            };
        }

        protected override Dictionary<int, ISpell> GetLockedSpells()
        {
            return new Dictionary<int, ISpell> {
                {8, SpellLexicon.Light.Hama},
                {15, SpellLexicon.Darkness.Mudo}, 
            };
        }
    }
}