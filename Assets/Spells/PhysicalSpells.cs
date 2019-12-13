
using Assets.Enums;

namespace Assets.Spells
{
    public abstract class PhysicalSpell: OffensiveSpell
    {
        public sealed override bool IsMagical => false;
        public sealed override Elements Element => Elements.Physical;
        public abstract int HitCount { get; }
        public abstract float CriticalChance { get; }
    }
}