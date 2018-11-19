namespace Assets.Spells
{
    public abstract class SpellBase
    {
        protected abstract string Id { get; }
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract int Cost { get; }
        public abstract bool IsMultitarget { get; } 
        public abstract bool IsMagical { get; }
        public bool IsPhysical => !IsMagical;
    }
}