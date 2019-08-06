
using UnityEngine;

namespace Assets.CharacterSystem.PassiveSkills.RecoverySkills {
    public class Regenerate : PassiveSkillsBase {
        public float Amount { get; }
        public override string Name { get; protected set; }
        public override string Description => $"Restore {Amount*100}% of max HP each turn in battle.";
        public override Phase ActivationPhase => Phase.Turn;
        
        private Regenerate (string name, float amount) {
            Name = name;
            Amount = amount;
            IsActive = true;
        }

        public static Regenerate GetRegenerate (Options option) {
            switch (option)
            {
                case Options.One:
                    return new Regenerate("Regenerate 1", 0.02f);
                case Options.Two:
                    return new Regenerate("Regenerate 2", 0.04f);
                case Options.Three:
                    return new Regenerate("Regenerate 3", 0.06f);
            }

            return null;
        }

        public override void Activate (Character character) {
            character.CurrentHP += Mathf.CeilToInt(character.Hp * Amount);
        }

        public override void Terminate (Character character) {}

        public enum Options {
            One,
            Two,
            Three
        }
    }
}