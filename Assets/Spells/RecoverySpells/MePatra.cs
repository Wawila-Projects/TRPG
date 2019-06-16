using System.Collections.Generic;
using Asstes.CharacterSystem.StatusEffects;

namespace Assets.Spells.RecoverySpells
{
    public class MePatra : RecoverySpell, IAssitSpell
    {
        protected override string Id => "Assist1";
        public override string Name => "Me Patra";
        public override string Description => "Cures Rage/Fear/Sleep/Silence/Confusion/Charm of party.";
        public override int Cost => 6;
        public override bool IsMultitarget => true;
        public List<StatusConditions> CureableStatusConditions => new List<StatusConditions> {
            StatusConditions.Rage, StatusConditions.Fear, StatusConditions.Sleep, 
            StatusConditions.Silence, StatusConditions.Confusion, StatusConditions.Charm,
        };
    }
}