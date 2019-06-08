using Asstes.CharacterSystem;

namespace Assets.Spells.AilementSpells {
    public class Taunt : AilementSpell {
        protected override string Id => "Ailement6";
        public override string Name => "Taunt";
        public override bool IsMultitarget => false;
        public override StatusConditions StatusConditionInflicted => StatusConditions.Rage;

    }
}