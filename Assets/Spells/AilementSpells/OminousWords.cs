using Asstes.CharacterSystem;

namespace Assets.Spells.AilementSpells {
    public class OminousWords : AilementSpell {
        protected override string Id => "Ailement8";
        public override string Name => "Ominous Words";
        public override bool IsMultitarget => false;
        public override StatusConditions StatusConditionInflicted => StatusConditions.Rage;

    }
}