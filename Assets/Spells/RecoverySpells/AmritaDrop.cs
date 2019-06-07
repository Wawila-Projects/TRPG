using System.Linq;
using System.Collections.Generic;
using Assets.Utils;
using Asstes.CharacterSystem;


namespace Assets.Spells.RecoverySpells
{
    public class AmritaDrop : RecoverySpell, IAssitSpell
    {
        protected override string Id => "Assist7";
        public override string Name => "Amrita Drop";
        public override string Description => "Cure all ailments of 1 ally.";
        public override int Cost => 8;
        public override bool IsMultitarget => false;
        public List<StatusConditions> CureableStatusConditions => EnumUtils<StatusConditions>.ToList();
    }
}