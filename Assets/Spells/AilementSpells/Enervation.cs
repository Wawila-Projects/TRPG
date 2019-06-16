using Asstes.CharacterSystem.StatusEffects;

namespace Assets.Spells.AilementSpells {
    public class Enervation : AilementSpell {
        protected override string Id => "Ailement4";
        public override string Name => "Enervation";
        public override bool IsMultitarget => false;
        public override StatusConditions StatusConditionInflicted => StatusConditions.Enervation;

    }
}