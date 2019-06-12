using System.Collections.Generic;
using Assets.Spells;
using Assets.Spells.SpellLexicon;
using Assets.Enums;

namespace Assets.Personas.Magician {
    public class Jiraiya : PersonaBase
    {
        public override string Name => "Jiraiya";
        public override bool IsPlayerPersona => true;
        public override Arcana Arcana => Arcana.Magician;
        public override Elements InheritanceElement => Elements.Almighty;

        protected override void Awake() {
            Level = 1;
            base.Awake();
        }
        protected override IDictionary<Statistics, int> GetBaseStats()
        {   
            return new Dictionary<Statistics, int> {
                {Statistics.Strength,   6}, 
                {Statistics.Magic,      9},
                {Statistics.Endurance,  7}, 
                {Statistics.Agility,    8}, 
                {Statistics.Luck,       5}
            };       
        }

        protected override void SetResistances()
        {
            Resistances[Elements.Wind] = ResistanceModifiers.Resist;
            Resistances[Elements.Elec] = ResistanceModifiers.Weak;
        }

        protected override List<SpellBase> GetBaseSpellbook()
        {
            return new List<SpellBase> {
                SpellLexicon.Wind.Garu, 
                SpellLexicon.Physical.Bash, 
            };
        }

        protected override Dictionary<int, SpellBase> GetLockedSpells()
        {
            return new Dictionary<int, SpellBase> {
                {10, SpellLexicon.Wind.Magaru},
                {15, SpellLexicon.Wind.Garula}
            };
        }
    }
}