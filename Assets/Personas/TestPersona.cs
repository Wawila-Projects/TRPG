using System.Collections.Generic;
using Assets.Spells;
using Assets.Spells.SpellLexicon;
using Assets.Take_II.Scripts.Combat;
using Assets.Take_II.Scripts.Enums;
using Assets.Take_II.Scripts.PlayerManager;

namespace Assets.Personas {
    public class TestPersona : PersonaBase
    {
        public override string Name => "TestPersona";
        TestPersona() : base(Arcana.Fool, Elements.Almighty) { }
        protected override Stats GetBaseStats()
        {
            var stats = new Stats() {
                Level = 1, 
                Strength = 30, 
                Magic = 30, 
                Endurance = 30, 
                Agility = 30, 
                Luck = 30,
            };

            stats.Resistances[Elements.Fire] = Resistances.Resist;
            stats.Resistances[Elements.Ice] = Resistances.Absorb;
            stats.Resistances[Elements.Wind] = Resistances.Weak;
            stats.Resistances[Elements.Elec] = Resistances.Block;
            return stats;
        }

        protected override List<SpellBase> GetBaseSpellbook()
        {
            return new List<SpellBase> {
                SpellLexicon.Fire.Agilao.Value,
                SpellLexicon.Wind.Magarula.Value,
                SpellLexicon.Elec.Zionga.Value, 
                SpellLexicon.Ice.Mabufula.Value
            };
        }

        protected override Dictionary<int, SpellBase> GetLockedSpells()
        {
            return new Dictionary<int, SpellBase> {
                
            };
        }
    }
}