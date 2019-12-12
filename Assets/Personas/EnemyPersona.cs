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
                {Statistics.Strength,   4}, 
                {Statistics.Magic,      2},
                {Statistics.Endurance,  3}, 
                {Statistics.Agility,    5}, 
                {Statistics.Luck,       3}
            };       
        }

        protected override void SetResistances()
        {
            // Resistances[Elements.Ice] = ResistanceModifiers.None;
            // Resistances[Elements.Elec] = ResistanceModifiers.None;
            // Resistances[Elements.Wind] = ResistanceModifiers.None;
            // Resistances[Elements.Fire] = ResistanceModifiers.None;
            // Resistances[Elements.Physical] = ResistanceModifiers.None;
            // Resistances[Elements.Almighty] = ResistanceModifiers.None;
        }

        protected override List<ISpell> GetBaseSpellbook()
        {
            return new List<ISpell> {
                SpellLexicon.Fire.Agi, 
                SpellLexicon.Ice.Bufu, 
                SpellLexicon.Wind.Garu, 
                SpellLexicon.Elec.Zio, 
                SpellLexicon.Physical.Bash,
                SpellLexicon.Recovery.Dia, 
            };
        }

        protected override Dictionary<int, ISpell> GetLockedSpells()
        {
            return new Dictionary<int, ISpell> {

            };
        }
    }
}