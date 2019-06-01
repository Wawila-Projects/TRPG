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
            Level = 55;
            base.Awake();
        }
        protected override IDictionary<Statistics, int> GetBaseStats()
        {   
            return new Dictionary<Statistics, int> {
                {Statistics.Strength,   32}, 
                {Statistics.Magic,      38},
                {Statistics.Endurance,  32}, 
                {Statistics.Agility,    40}, 
                {Statistics.Luck,       31}
            };       
        }

        protected override void SetResistances()
        {
            Resistances[Elements.Bless] = ResistanceModifiers.Resist;
            Resistances[Elements.Curse] = ResistanceModifiers.Resist;
        }

        protected override List<SpellBase> GetBaseSpellbook()
        {
            return new List<SpellBase> {
                SpellLexicon.Fire.Agidyne, 
                SpellLexicon.Wind.Garudyne, 
                SpellLexicon.Darkness.Mudoon, 
                SpellLexicon.Light.Hamaon, 
                SpellLexicon.Almighty.Megidola,
            };
        }

        protected override Dictionary<int, SpellBase> GetLockedSpells()
        {
            return new Dictionary<int, SpellBase> {
                {68, SpellLexicon.Darkness.Mamudoon}, 
                {70, SpellLexicon.Light.Mahamaon},
                {75, SpellLexicon.Almighty.Megidolaon},
            };
        }
    }
}