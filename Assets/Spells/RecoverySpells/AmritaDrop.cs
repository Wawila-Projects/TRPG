using System.Collections.Generic;
using System.Linq;
using Assets.Utils;
using Asstes.CharacterSystem.StatusEffects;

namespace Assets.Spells.RecoverySpells {
    public class AmritaDrop : RecoverySpell, IAssitSpell {
        protected override string Id => "Assist7";
        public override string Name => "Amrita Drop";
        public override string Description => "Cure all ailments of 1 ally.";
        public override int Cost => 8;
        public override bool IsMultitarget => false;
        public List<StatusCondition> CureableStatusConditions => EnumUtils<StatusCondition>.GetValues ().Where (
            (sc) => sc != StatusCondition.None || sc != StatusCondition.Burn ||
            sc != StatusCondition.Dizzy || sc != StatusCondition.Down ||
            sc != StatusCondition.Shock || sc != StatusCondition.Freeze
        ).ToList ();
    }
}