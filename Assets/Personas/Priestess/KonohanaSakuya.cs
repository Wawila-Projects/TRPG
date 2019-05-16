using System.Collections.Generic;
using Assets.Spells;
using Assets.Spells.SpellLexicon;
using Assets.Take_II.Scripts.Enums;

namespace Assets.Personas.Priestess {
    public class KonohanaSakuya : PersonaBase
    {
        public override string Name => "Konohana Sakuya";
        public override bool IsPlayerPersona => true;

        public override Arcana Arcana => Arcana.Priestess;
        public override Elements InheritanceElement => Elements.Almighty;

        protected override void Awake() {
            Level = 15;
            base.Awake();
        }
        protected override void SetBaseStats()
        {   
            Stats = new Dictionary<Statistics, int> {
                {Statistics.Strength,   8}, 
                {Statistics.Magic,      15},
                {Statistics.Endurance,  11}, 
                {Statistics.Agility,    8}, 
                {Statistics.Luck,       10}
            };       
        }

        protected override void SetResistances()
        {
            Resistances[Elements.Fire] = ResistanceModifiers.Resist;
            Resistances[Elements.Ice] = ResistanceModifiers.Weak;
        }

        protected override List<SpellBase> GetBaseSpellbook()
        {
            return new List<SpellBase> {
                SpellLexicon.Fire.Agi, 
                SpellLexicon.Fire.Maragi, 
            };
        }

        protected override Dictionary<int, SpellBase> GetLockedSpells()
        {
            return new Dictionary<int, SpellBase> {
                {21, SpellLexicon.Fire.Agilao},
                {39, SpellLexicon.Fire.Maragion}
            };
        }
    }
}