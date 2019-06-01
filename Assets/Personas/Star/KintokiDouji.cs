using System.Collections.Generic;
using Assets.Spells;
using Assets.Spells.SpellLexicon;
using Assets.Enums;

namespace Assets.Personas.Star {
    public class KintokiDouji : PersonaBase
    {
        public override string Name => "Kintoki-Douji";
        public override bool IsPlayerPersona => true;

        public override Arcana Arcana => Arcana.Star;
        public override Elements InheritanceElement => Elements.Almighty;

        protected override void Awake() {
            Level = 35;
            base.Awake();
        }
        protected override IDictionary<Statistics, int> GetBaseStats()
        {   
            return new Dictionary<Statistics, int> {
                {Statistics.Strength,   24}, 
                {Statistics.Magic,      25},
                {Statistics.Endurance,  20}, 
                {Statistics.Agility,    22}, 
                {Statistics.Luck,       16}
            };       
        }

        protected override void SetResistances()
        {
            Resistances[Elements.Ice] = ResistanceModifiers.Resist;
            Resistances[Elements.Elec] = ResistanceModifiers.Weak;
        }

        protected override List<SpellBase> GetBaseSpellbook()
        {
            return new List<SpellBase> {
                SpellLexicon.Ice.Bufula, 
            };
        }

        protected override Dictionary<int, SpellBase> GetLockedSpells()
        {
            return new Dictionary<int, SpellBase> {
                {38, SpellLexicon.Ice.Mabufula},
            };
        }
    }
}