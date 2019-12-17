using Asstes.CharacterSystem.StatusEffects;

namespace Assets.Spells.AilementSpells {
    public class EvilSmile : AilementSpell {
        protected override string Id => "Ailement1";
        public override string Name => "Evil Smile";
        public override bool IsMultitarget => true;
        public override StatusCondition StatusConditionInflicted => StatusCondition.Fear;

    }
}