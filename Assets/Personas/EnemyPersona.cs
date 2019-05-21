using System.Collections.Generic;
using Assets.Spells;
using Assets.Spells.SpellLexicon;
using Assets.Enums;

namespace Assets.Personas {
    public class EnemyPersona : PersonaBase
    {
        public override string Name => "Shadow";
        public override bool IsPlayerPersona => false;
        public override Arcana Arcana => Arcana.Death;
        public override Elements InheritanceElement => Elements.Almighty;

        protected override void Awake() {
            Level = 4;
            base.Awake();
        }
        protected override void SetBaseStats()
        {   
            Stats = new Dictionary<Statistics, int> {
                {Statistics.Strength,   5}, 
                {Statistics.Magic,      3},
                {Statistics.Endurance,  3}, 
                {Statistics.Agility,    4}, 
                {Statistics.Luck,       4}
            };       
        }

        protected override void SetResistances()
        {
            Resistances[Elements.Elec] = ResistanceModifiers.Reflect;
            Resistances[Elements.Wind] = ResistanceModifiers.Absorb;
            Resistances[Elements.Fire] = ResistanceModifiers.Block;
            Resistances[Elements.Physical] = ResistanceModifiers.Resist;
            Resistances[Elements.Ice] = ResistanceModifiers.Weak;
            Resistances[Elements.Almighty] = ResistanceModifiers.Block;
        }

        protected override List<SpellBase> GetBaseSpellbook()
        {
            return new List<SpellBase> {
                SpellLexicon.Fire.Agi, 
                SpellLexicon.Ice.Bufu, 
                SpellLexicon.Wind.Garu, 
                SpellLexicon.Elec.Zio, 
                SpellLexicon.Physical.Bash, 
            };
        }

        protected override Dictionary<int, SpellBase> GetLockedSpells()
        {
            return new Dictionary<int, SpellBase> {

            };
        }
    }
}