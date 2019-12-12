using Asstes.CharacterSystem.StatusEffects;
using Assets.Enums;

namespace Assets.Spells
{
    public abstract class AilementSpell: CastableSpell
    {
        public override bool IsMagical => true;
        public override Elements Element => Elements.Ailment;
        public override int Cost => IsMultitarget ? 5 : 12;
        public override string Description => 
            $"Inflicts {StatusConditionInflicted.ToString()} to {(IsMultitarget ? "all foes" : "1 foe")}.";
        public float HitChange => IsMultitarget ? 0.3f : 0.4f;
        public abstract StatusConditions StatusConditionInflicted { get; }
    }
}