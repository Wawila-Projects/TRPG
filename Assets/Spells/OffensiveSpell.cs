using Assets.Take_II.Scripts.Enums;

namespace Assets.Spells
{
    public abstract class OffensiveSpell: SpellBase
    {
        public abstract int AttackPower { get; }
        public abstract int Accuracy { get; }
    }
}