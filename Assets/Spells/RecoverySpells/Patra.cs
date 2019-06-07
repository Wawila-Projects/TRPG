using System.Collections.Generic;
using Asstes.CharacterSystem;

namespace Assets.Spells.RecoverySpells
{
    public class Patra : RecoverySpell, IAssitSpell
    {
        protected override string Id => "Assist0";
        public override string Name => "Patra";
        public override string Description => "Cures Rage/Fear/Sleep/Silence/Confusion/Charm of 1 ally.";
        public override int Cost => 3;
        public override bool IsMultitarget => false;
        public List<StatusConditions> CureableStatusConditions => new List<StatusConditions> {
            StatusConditions.Rage, StatusConditions.Fear, StatusConditions.Sleep, 
            StatusConditions.Silence, StatusConditions.Confusion, StatusConditions.Charm,
        };
    }
}