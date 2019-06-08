using Asstes.CharacterSystem;

namespace Assets.Spells.AilementSpells {
    public class Pulinpa : AilementSpell {
        protected override string Id => "Ailement2";
        public override string Name => "Pulinpa";
        public override bool IsMultitarget => false;
        public override StatusConditions StatusConditionInflicted => StatusConditions.Confusion;

    }
}