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