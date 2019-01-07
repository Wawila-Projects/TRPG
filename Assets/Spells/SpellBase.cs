using System;
using Assets.Take_II.Scripts.Enums;

namespace Assets.Spells
{
    public abstract class SpellBase: IEquatable<SpellBase>
    {
        protected abstract string Id { get; }
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract int Cost { get; }
        public abstract bool IsMultitarget { get; } 
        public abstract bool IsMagical { get; }
        public bool IsPhysical => !IsMagical;
        public abstract Elements Element { get; }

        public bool Equals(SpellBase other)
        {
            if (other == null) return false;
            return this.Id == other.Id;
        }
    }
}