using Asstes.CharacterSystem.StatusEffects;

namespace Assets.Spells.AilementSpells {
    public class SexyDance : AilementSpell {
        protected override string Id => "Ailement11";
        public override string Name => "Sexy Dance";
        public override bool IsMultitarget => true;
        public override StatusConditions StatusConditionInflicted => StatusConditions.Charm;

    }
}