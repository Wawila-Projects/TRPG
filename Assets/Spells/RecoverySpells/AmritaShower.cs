using System.Collections.Generic;
using System.Linq;
using Assets.Utils;
using Asstes.CharacterSystem.StatusEffects;

namespace Assets.Spells.RecoverySpells {
    public class AmritaShower : RecoverySpell, IAssitSpell {
        protected override string Id => "Assist8";
        public override string Name => "Amrita Shower";
        public override string Description => "Cure all ailments of party.";
        public override int Cost => 16;
        public override bool IsMultitarget => true;
        public List<StatusCondition> CureableStatusConditions =>
            EnumUtils<StatusCondition>.GetValues ().Where (
                (sc) => sc != StatusCondition.None || sc != StatusCondition.Burn ||
                sc != StatusCondition.Dizzy || sc != StatusCondition.Down ||
                sc != StatusCondition.Shock || sc != StatusCondition.Freeze
            ).ToList ();
    }
}