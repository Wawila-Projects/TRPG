using System;
using Assets.Enums;
using Assets.CharacterSystem;

namespace Assets.Spells
{
    // TODO Check descriptions.. "of Party" to ~MultiTarget
    public abstract class CastableSpell: ISpell, IEquatable<CastableSpell>
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
            var cost = Cost;
            if (IsMagical) {
                cost = character.Persona.SpellMaster ? (int)Math.Ceiling(cost/2f) : cost;
                character.CurrentSP -= cost;
                return;
            }

            cost = (int)Math.Ceiling(character.Hp * (Cost/100f));
            cost = character.Persona.ArmsMaster ? (int)Math.Ceiling(cost/2f) : cost;
            character.CurrentHP -= cost;
        }

        public virtual bool CanBeCasted(Character character)
        {
            var cost = Cost;
            if (IsMagical) {
                cost = character.Persona.SpellMaster ? (int)Math.Ceiling(cost/2f) : cost;
                return character.CurrentSP >= cost;
            }

            cost = (int)Math.Ceiling(character.Hp * (Cost/100f));
            cost = character.Persona.ArmsMaster ? (int)Math.Ceiling(cost/2f) : cost;
            return character.CurrentHP > cost;
        }

        public bool Equals(CastableSpell other)
        {
            return Id == other?.Id;
        }

        public override bool Equals(Object other)
        {
            var spell = other as CastableSpell;
            return Id == spell?.Id;
        }

        public override int GetHashCode() {
            return Id.GetHashCode();
        }

        public static bool operator == (CastableSpell lhs, CastableSpell rhs) 
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
        public static bool operator != (CastableSpell lhs, CastableSpell rhs) 
        {
            return !(lhs == rhs);
        }
    }
}