using System.Collections.Generic;
using Assets.Spells;
using Assets.Spells.SpellLexicon;
using Assets.Enums;
using Assets.Spells.AilementSpells;

namespace Assets.Personas.Star {
    public class KintokiDouji : PersonaBase
    {
        public override string Name => "Kintoki-Douji";
        public override bool IsPlayerPersona => true;

        public override Arcana Arcana => Arcana.Star;
        public override Elements InheritanceElement => Elements.Almighty;

        protected override void Awake() {
            Level = 1;
            base.Awake();
        }
        protected override IDictionary<Statistics, int> GetBaseStats()
        {   
            return new Dictionary<Statistics, int> {
                {Statistics.Strength,   6}, 
                {Statistics.Magic,      8},
                {Statistics.Endurance,  5}, 
                {Statistics.Agility,    4}, 
                {Statistics.Luck,       9}
            };       
        }

        protected override void SetResistances()
        {
            Resistances[Elements.Ice] = ResistanceModifiers.Resist;
            Resistances[Elements.Elec] = ResistanceModifiers.Weak;
        }

        protected override List<ISpell> GetBaseSpellbook()
        {
            return new List<ISpell> {
                SpellLexicon.Ice.Bufu, 
                new Lullaby(),
                SpellLexicon.Recovery.Dia, 
            };
        }

        protected override Dictionary<int, ISpell> GetLockedSpells()
        {
            return new Dictionary<int, ISpell> {
                {8, SpellLexicon.Recovery.AmritaDrop},
                {15, SpellLexicon.Ice.Mabufu},
            };
        }
    }
}