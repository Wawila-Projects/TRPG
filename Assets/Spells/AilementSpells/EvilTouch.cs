using Asstes.CharacterSystem.StatusEffects;

namespace Assets.Spells.AilementSpells {
    public class EvilTouch : AilementSpell {
        protected override string Id => "Ailement0";
        public override string Name => "Evil Touch";
        public override bool IsMultitarget => false;
        public override StatusConditions StatusConditionInflicted => StatusConditions.Fear;

    }
}