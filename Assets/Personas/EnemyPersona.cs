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
            Level = 2;
            base.Awake();
        }
        protected override IDictionary<Statistics, int> GetBaseStats()
        {   
            return new Dictionary<Statistics, int> {
                {Statistics.Strength,   8}, 
                {Statistics.Magic,      10},
                {Statistics.Endurance,  7}, 
                {Statistics.Agility,    8}, 
                {Statistics.Luck,       9}
            };       
        }

        protected override void SetResistances()
        {
        //     Resistances[Elements.Elec] = ResistanceModifiers.Weak;
        //     Resistances[Elements.Wind] = ResistanceModifiers.Weak;
        //     Resistances[Elements.Fire] = ResistanceModifiers.Weak;
        //     Resistances[Elements.Ice] = ResistanceModifiers.Weak;
        //     Resistances[Elements.Physical] = ResistanceModifiers.Weak;
        //     Resistances[Elements.Almighty] = ResistanceModifiers.Weak;
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