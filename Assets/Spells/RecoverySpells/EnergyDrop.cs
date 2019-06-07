using System.Collections.Generic;
using Asstes.CharacterSystem;

namespace Assets.Spells.RecoverySpells
{
    public class EnergyDrop : RecoverySpell, IAssitSpell
    {
        protected override string Id => "Assist3";
        public override string Name => "Energy Drop";
        public override string Description => "Cure Despair/Enervation/Distress of 1 ally.";
        public override int Cost => 6;
        public override bool IsMultitarget => false;
        public List<StatusConditions> CureableStatusConditions => new List<StatusConditions> {
            StatusConditions.Dispair,  StatusConditions.Enervation,  StatusConditions.Distress, 
        };
    }
}