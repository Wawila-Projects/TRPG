using System.Collections.Generic;
using Assets.Spells;
using Assets.Spells.SpellLexicon;
using Assets.Enums;

namespace Assets.Personas.Priestess {
    public class KonohanaSakuya : PersonaBase
    {
        public override string Name => "Konohana Sakuya";
        public override bool IsPlayerPersona => true;

        public override Arcana Arcana => Arcana.Priestess;
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
                {Statistics.Agility,    5}, 
                {Statistics.Luck,       8}
            };       
        }

        protected override void SetResistances()
        {
            Resistances[Elements.Fire] = ResistanceModifiers.Resist;
            Resistances[Elements.Ice] = ResistanceModifiers.Weak;
        }

        protected override List<ISpell> GetBaseSpellbook()
        {
            return new List<ISpell> {
                SpellLexicon.Fire.Agi, 
                SpellLexicon.Fire.Maragi, 
                SpellLexicon.Recovery.Dia, 
            };
        }

        protected override Dictionary<int, ISpell> GetLockedSpells()
        {
            return new Dictionary<int, ISpell> {
                {8, SpellLexicon.Recovery.Media},
                {15, SpellLexicon.Fire.Maragi},
            };
        }
    }
}