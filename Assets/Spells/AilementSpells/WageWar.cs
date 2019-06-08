using Asstes.CharacterSystem;

namespace Assets.Spells.AilementSpells {
    public class WageWar : AilementSpell {
        protected override string Id => "Ailement7";
        public override string Name => "Wage War";
        public override bool IsMultitarget => true;
        public override StatusConditions StatusConditionInflicted => StatusConditions.Rage;

    }
}