using Asstes.CharacterSystem;

namespace Assets.Spells.AilementSpells {
    public class OldOne : AilementSpell {
        protected override string Id => "Ailement5";
        public override string Name => "Old One";
        public override bool IsMultitarget => true;
        public override StatusConditions StatusConditionInflicted => StatusConditions.Enervation;

    }
}