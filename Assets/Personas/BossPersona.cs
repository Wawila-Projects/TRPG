using System.Collections.Generic;
using Assets.Spells;
using Assets.Spells.SpellLexicon;
using Assets.Enums;
using Assets.CharacterSystem.PassiveSkills.RecoverySkills;

namespace Assets.Personas {
    public class BossPersona : PersonaBase
    {
        public override string Name => "Boss";
        public override bool IsPlayerPersona => false;
        public override Arcana Arcana => Arcana.Death;
        public override Elements InheritanceElement => Elements.None;

        protected override void Awake() {
            Level = 2;
            base.Awake();
        }
        protected override IDictionary<Statistics, int> GetBaseStats()
        {   
            return new Dictionary<Statistics, int> {
                {Statistics.Strength,   5}, 
                {Statistics.Magic,      5},
                {Statistics.Endurance,  7}, 
                {Statistics.Agility,    3}, 
                {Statistics.Luck,       3}
            };       
        }

        protected override void SetResistances()
        {
            Resistances[Elements.Bless] = ResistanceModifiers.Block;
            Resistances[Elements.Curse] = ResistanceModifiers.Block;
            // Resistances[Elements.Wind] = ResistanceModifiers.None;
            // Resistances[Elements.Fire] = ResistanceModifiers.None;
            // Resistances[Elements.Physical] = ResistanceModifiers.None;
            // Resistances[Elements.Almighty] = ResistanceModifiers.None;
        }

        protected override List<ISpell> GetBaseSpellbook()
        {
            return new List<ISpell> {
                SpellLexicon.Fire.Agilao, 
                SpellLexicon.Ice.Bufula, 
                SpellLexicon.Wind.Garula, 
                SpellLexicon.Elec.Zionga, 
                SpellLexicon.Physical.Bash,
                Regenerate.GetRegenerate(Regenerate.Options.One),
            };
        }

        protected override Dictionary<int, ISpell> GetLockedSpells()
        {
            return new Dictionary<int, ISpell> {

            };
        }
    }
}