using Asstes.CharacterSystem;

namespace Assets.Spells.AilementSpells {
    public class Dormina : AilementSpell {
        protected override string Id => "Ailement12";
        public override string Name => "Dormina";
        public override bool IsMultitarget => false;
        public override StatusConditions StatusConditionInflicted => StatusConditions.Sleep;

    }
}