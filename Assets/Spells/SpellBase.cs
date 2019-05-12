using System;
using Assets.Take_II.Scripts.Enums;
using Assets.Take_II.Scripts.GameManager;

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

        public virtual int HandleCostReduction(Character character)
        {
            var cost = Cost;
            if (IsPhysical) {
                cost = character.Hp * (Cost/100);
            }
            character.CurrentSpiritPoints -= cost;
            return cost;
        }

        public virtual bool CanBeCasted(Character character)
        {
            if (IsPhysical) {
                var cost = character.Hp * (Cost/100);
                return character.CurrentHealth > cost;
            }
            return character.CurrentSpiritPoints >= Cost;
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