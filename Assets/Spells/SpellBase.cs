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
            double cost = Cost;
            if (!IsPhysical) {
                cost = character.Persona.SpellMaster ? cost/2 : cost;
                character.CurrentSP -= (int)cost;
                return;
            }

            cost = Math.Ceiling(character.Hp * (Cost/100f));
            cost = character.Persona.ArmsMaster ? cost/2 : cost;
            character.CurrentHP -= (int)cost;
        }

        public virtual bool CanBeCasted(Character character)
        {
            double cost = Cost;
            if (!IsPhysical) {
                cost = character.Persona.SpellMaster ? cost/2 : cost;
                return character.CurrentSP >= cost;
            }

            cost = Math.Ceiling(character.Hp * (Cost/100f));
            cost = character.Persona.ArmsMaster ? cost/2f : cost;
            return character.CurrentHP > cost;
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