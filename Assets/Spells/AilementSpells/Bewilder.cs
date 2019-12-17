using Asstes.CharacterSystem.StatusEffects;

namespace Assets.Spells.AilementSpells {
    public class Bewilder : AilementSpell {
        protected override string Id => "Ailement14";
        public override string Name => "Bewilder";
        public override bool IsMultitarget => false;
        public override StatusCondition StatusConditionInflicted => StatusCondition.Distress;

    }
}