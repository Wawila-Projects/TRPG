using Asstes.CharacterSystem.StatusEffects;

namespace Assets.Spells.AilementSpells {
    public class Lullaby : AilementSpell {
        protected override string Id => "Ailement13";
        public override string Name => "Lullaby";
        public override bool IsMultitarget => true;
        public override StatusConditions StatusConditionInflicted => StatusConditions.Sleep;

    }
}