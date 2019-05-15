namespace Assets.Spells
{
    public abstract class OffensiveSpell: SpellBase
    {
        public abstract int AttackPower { get; }
        public abstract float Accuracy { get; }
    }
}