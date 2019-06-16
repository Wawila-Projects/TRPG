using Asstes.CharacterSystem.StatusEffects;

namespace Assets.Spells.AilementSpells {
    public class Tentarafoo : AilementSpell {
        protected override string Id => "Ailement3";
        public override string Name => "Tentarafoo";
        public override bool IsMultitarget => true;
        public override StatusConditions StatusConditionInflicted => StatusConditions.Confusion;

    }
}