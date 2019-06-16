using System.Collections.Generic;
using Asstes.CharacterSystem.StatusEffects;

namespace Assets.Spells.RecoverySpells
{
    public class MaBaisudi : RecoverySpell, IAssitSpell
    {
        protected override string Id => "Assist6";
        public override string Name => "Baisudi";
        public override string Description => "Cure Burn/Freeze/Shock of party.";
        public override int Cost => 8;
        public override bool IsMultitarget => true;
        public List<StatusConditions> CureableStatusConditions => new List<StatusConditions> {
            StatusConditions.Burn,  StatusConditions.Freeze,  StatusConditions.Shock, 
        };
    }
}