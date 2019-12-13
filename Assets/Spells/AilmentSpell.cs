using Assets.Enums;
using Asstes.CharacterSystem.StatusEffects;

namespace Assets.Spells {
    public abstract class AilementSpell : CastableSpell {
        public sealed override bool IsMagical => true;
        public sealed override Elements Element => Elements.Ailment;
        public override int Cost => IsMultitarget ? 5 : 12;
        public sealed override string Description =>
            $"Inflicts {StatusConditionInflicted.ToString()} to {(IsMultitarget ? "multiple foes" : "1 foe")}.";
        public float HitChange => IsMultitarget ? 0.3f : 0.4f;
        public abstract StatusConditions StatusConditionInflicted { get; }
    }
}