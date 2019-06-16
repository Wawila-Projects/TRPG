using System.Collections.Generic;
using Asstes.CharacterSystem.StatusEffects;

namespace Assets.Spells.RecoverySpells
{
    public class EnergyShower : RecoverySpell, IAssitSpell
    {
        protected override string Id => "Assist4";
        public override string Name => "Energy Shower";
        public override string Description => "Cure Despair/Enervation/Distress of party.";
        public override int Cost => 12;
        public override bool IsMultitarget => true;
        public List<StatusConditions> CureableStatusConditions => new List<StatusConditions> {
            StatusConditions.Dispair,  StatusConditions.Enervation,  StatusConditions.Distress, 
        };
    }
}