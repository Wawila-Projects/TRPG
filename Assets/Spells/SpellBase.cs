using System;
using Assets.Enums;
using Assets.CharacterSystem;

namespace Assets.Spells
{

    // TODO Check descriptions.. "of Party" to ~MultiTarget
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

        public override string ToString() => $"{Id} | {Element.ToString()} | {Name}";

        public virtual void HandleCostReduction(Character character)
        {
            if (!IsPhysical) {
                character.CurrentSP -= Cost;
                return;
            }

            var cost = (int) Math.Ceiling(character.Hp * (Cost/100f));
            character.CurrentHP -= cost;
        }

        public virtual bool CanBeCasted(Character character)
        {
            if (IsPhysical) {
                var cost = (int) Math.Ceiling(character.Hp * (Cost/100f));
                return character.CurrentHP > cost;
            }
            return character.CurrentSP >= Cost;
        }

        public bool Equals(SpellBase other)
        {
            return Id == other?.Id;
        }

        public override bool Equals(Object other)
        {
            var spell = other as SpellBase;
            return Id == spell?.Id;
        }

        public override int GetHashCode() {
            return Id.GetHashCode();
        }

        public static bool operator == (SpellBase lhs, SpellBase rhs) 
        {
            if (Object.ReferenceEquals (lhs, rhs)) 
            {
                return true;
            }
            if (lhs is null || rhs is null) 
            {
                return false;
            }
            return lhs.Id == rhs.Id;
        }
        public static bool operator != (SpellBase lhs, SpellBase rhs) 
        {
            return !(lhs == rhs);
        }
    }
}