using System.Collections.Generic;
using Asstes.CharacterSystem.StatusEffects;

namespace Assets.Spells.RecoverySpells
{
    public class Patra : RecoverySpell, IAssitSpell
    {
        protected override string Id => "Assist0";
        public override string Name => "Patra";
        public override string Description => "Cures Rage/Fear/Sleep/Silence/Confusion/Charm of 1 ally.";
        public override int Cost => 3;
        public override bool IsMultitarget => false;
        public List<StatusCondition> CureableStatusConditions => new List<StatusCondition> {
            StatusCondition.Rage, StatusCondition.Fear, StatusCondition.Sleep, 
            StatusCondition.Silence, StatusCondition.Confusion, StatusCondition.Charm,
        };
    }
}