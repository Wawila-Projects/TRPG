namespace Assets.Spells
{
    public abstract class OffensiveSpell: CastableSpell
    {
        public abstract int AttackPower { get; }
        public abstract float Accuracy { get; }
    }
}