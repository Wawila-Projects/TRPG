using System.Collections.Generic;
using Assets.Spells;
using Assets.Take_II.Scripts.Combat;
using Assets.Take_II.Scripts.Enums;
using Assets.Take_II.Scripts.PlayerManager;

namespace Assets.Personas.Fool {
    public class Arsene : PersonaBase
    {
        public override string Name => "Arsene";
    
        Arsene() : base(Arcana.Fool, Elements.Curse) { 
            Level = 1;
        }
        protected override void SetBaseStats()
        {   
            Stats = new Dictionary<Statistics, int> {
                {Statistics.Strength, 2}, 
                {Statistics.Magic, 2},
                {Statistics.Endurance, 2}, 
                {Statistics.Agility, 3}, 
                {Statistics.Luck, 1}
            };       
        }

        protected override void SetResistances()
        {
            Resistances[Elements.Curse] = ResistanceModifiers.Resist;
            Resistances[Elements.Ice] = ResistanceModifiers.Weak;
            Resistances[Elements.Bless] = ResistanceModifiers.Weak;
        }

        protected override List<SpellBase> GetBaseSpellbook()
        {
            return new List<SpellBase> {

            };
        }

        protected override Dictionary<int, SpellBase> GetLockedSpells()
        {
            return new Dictionary<int, SpellBase> {
                
            };
        }
    }
}