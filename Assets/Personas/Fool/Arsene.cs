using System.Collections.Generic;
using Assets.Spells;
using Assets.Take_II.Scripts.Combat;
using Assets.Take_II.Scripts.Enums;
using Assets.Take_II.Scripts.PlayerManager;

namespace Assets.Personas.Fool {
    public class Arsene : PersonaBase
    {
        public override string Name => "Arsene";
        Arsene() : base(Arcana.Fool, Elements.Curse) { }
        protected override Stats GetBaseStats()
        {
            var stats = new Stats() {
                Level = 1, 
                Strength = 2, 
                Magic = 2, 
                Endurance = 2, 
                Agility = 3, 
                Luck = 1,
            };

            stats.Resistances[Elements.Curse] = Resistances.Resist;
            stats.Resistances[Elements.Ice] = Resistances.Weak;
            stats.Resistances[Elements.Bless] = Resistances.Weak;
            return stats;
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