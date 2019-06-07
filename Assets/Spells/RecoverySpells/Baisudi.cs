using System.Collections.Generic;
using Asstes.CharacterSystem;

namespace Assets.Spells.RecoverySpells
{
    public class Baisudi : RecoverySpell, IAssitSpell
    {
        protected override string Id => "Assist5";
        public override string Name => "Baisudi";
        public override string Description => "Cure Burn/Freeze/Shock of 1 ally.";
        public override int Cost => 4;
        public override bool IsMultitarget => false;
        public List<StatusConditions> CureableStatusConditions => new List<StatusConditions> {
            StatusConditions.Burn,  StatusConditions.Freeze,  StatusConditions.Shock, 
        };
    }
}