using Asstes.CharacterSystem.StatusEffects;

namespace Assets.Spells.AilementSpells {
    public class EerieSound : AilementSpell {
        protected override string Id => "Ailement15";
        public override string Name => "Eerie Sound";
        public override bool IsMultitarget => true;
        public override StatusConditions StatusConditionInflicted => StatusConditions.Distress;

    }
}