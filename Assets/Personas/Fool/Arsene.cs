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
                Strength = 2; 
                Magic = 2;
                Endurance = 2; 
                Agility = 3; 
                Luck = 1;
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