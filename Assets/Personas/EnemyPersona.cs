using System.Collections.Generic;
using Assets.Spells;
using Assets.Spells.SpellLexicon;
using Assets.Enums;

namespace Assets.Personas {
    public class EnemyPersona : PersonaBase
    {
        public override string Name => "Lying Hablerie";
        public override bool IsPlayerPersona => false;
        public override Arcana Arcana => Arcana.Magician;
        public override Elements InheritanceElement => Elements.None;

        protected override void Awake() {
            Level = 2;
            base.Awake();
        }
        protected override IDictionary<Statistics, int> GetBaseStats()
        {   
            return new Dictionary<Statistics, int> {
                {Statistics.Strength,   5}, 
                {Statistics.Magic,      2},
                {Statistics.Endurance,  8}, 
                {Statistics.Agility,    7}, 
                {Statistics.Luck,       5}
            };       
        }

        protected override void SetResistances()
        {
            Resistances[Elements.Ice] = ResistanceModifiers.Reflect;
            Resistances[Elements.Elec] = ResistanceModifiers.Weak;
            Resistances[Elements.Wind] = ResistanceModifiers.Resist;
            Resistances[Elements.Fire] = ResistanceModifiers.Block;
            Resistances[Elements.Physical] = ResistanceModifiers.Absorb;
            Resistances[Elements.Almighty] = ResistanceModifiers.None;
        }

        protected override List<SpellBase> GetBaseSpellbook()
        {
            return new List<SpellBase> {
                SpellLexicon.Fire.Maragi, 
                SpellLexicon.Ice.Mabufu, 
                SpellLexicon.Wind.Magaru, 
                SpellLexicon.Elec.Mazio, 
                SpellLexicon.Physical.Bash,
                SpellLexicon.Recovery.Dia, 
            };
        }

        protected override Dictionary<int, SpellBase> GetLockedSpells()
        {
            return new Dictionary<int, SpellBase> {

            };
        }
    }
}