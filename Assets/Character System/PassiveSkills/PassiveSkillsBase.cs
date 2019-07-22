using System;

namespace Assets.CharacterSystem.PassiveSkills {
    public abstract class PassiveSkillsBase : IEquatable<PassiveSkillsBase> {
        public bool IsActive = true;
        public abstract string Name { get; protected set; }
        public abstract string Description { get; }
        public abstract Phase ActivationPhase { get; }
        public abstract void Activate (Character character);
        public abstract void Terminate (Character character);

        public bool Equals (PassiveSkillsBase other) {
            if (ReferenceEquals (this, other))
                return true;

            if (ReferenceEquals (null, other))
                return false;

            return Name == other.Name;
        }

        public override bool Equals (object obj) {
            if (obj is PassiveSkillsBase peb) {
                return Equals (peb);
            }
            return false;
        }

        public override int GetHashCode () {
            return Name.GetHashCode ();
        }

        public static bool operator == (PassiveSkillsBase left, PassiveSkillsBase right) {
            if (ReferenceEquals (null, left))
                return false;
            return left.Equals (right);
        }

        public static bool operator != (PassiveSkillsBase left, PassiveSkillsBase right) {
            return !(left == right);
        }

        public enum Phase {
            Start, Turn, End
        }
    }
}