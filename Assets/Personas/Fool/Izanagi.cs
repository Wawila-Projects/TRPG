using System.Collections.Generic;
using Assets.Spells;
using Assets.Spells.SpellLexicon;
using Assets.Enums;

namespace Assets.Personas.Fool {
    public class Izanagi : PersonaBase
    {
        public override string Name => "Izanagi";
        public override bool IsPlayerPersona => true;

        public override Arcana Arcana => Arcana.Fool;
        public override Elements InheritanceElement => Elements.Almighty;

        protected override void Awake() {
            Level = 1;
            base.Awake();
        }
        protected override IDictionary<Statistics, int> GetBaseStats()
        {   
            return new Dictionary<Statistics, int> {
                {Statistics.Strength,   7}, 
                {Statistics.Magic,      8},
                {Statistics.Endurance,  8}, 
                {Statistics.Agility,    6}, 
                {Statistics.Luck,       7}
            };       
        }

        protected override void SetResistances()
        {
            Resistances[Elements.Curse] = ResistanceModifiers.Resist;
            Resistances[Elements.Ice] = ResistanceModifiers.Weak;
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
                {8, SpellLexicon.Elec.Mazio},
                {15, SpellLexicon.Physical.SonicPunch},
            };
        }
    }
}