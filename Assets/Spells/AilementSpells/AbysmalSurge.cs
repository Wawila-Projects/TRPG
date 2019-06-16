using Asstes.CharacterSystem.StatusEffects;

namespace Assets.Spells.AilementSpells {
    public class AbysmalSurge : AilementSpell {
        protected override string Id => "Ailement9";
        public override string Name => "Abysmal Surge";
        public override bool IsMultitarget => true;
        public override StatusConditions StatusConditionInflicted => StatusConditions.Dispair;

    }
}