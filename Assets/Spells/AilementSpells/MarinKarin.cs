using Asstes.CharacterSystem.StatusEffects;

namespace Assets.Spells.AilementSpells {
    public class MarinKarin : AilementSpell {
        protected override string Id => "Ailement10";
        public override string Name => "Marin Karin";
        public override bool IsMultitarget => false;
        public override StatusCondition StatusConditionInflicted => StatusCondition.Charm;

    }
}