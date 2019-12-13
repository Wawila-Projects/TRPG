
using UnityEngine;

namespace Assets.CharacterSystem.PassiveSkills.RecoverySkills {
    public class Invigorate : PassiveSkillsBase {
        public int Amount { get; }
        public override string Name { get; protected set; }
        public override string Description => $"Recover {Amount} SP each turn in battle.";
        public override Phase ActivationPhase => Phase.Turn;
        
        private Invigorate (string name, int amount) {
            Name = name;
            Amount = amount;
            IsActive = true;
        }

        public static Invigorate GetInvigorate (Options option) {
            switch (option)
            {
                case Options.One:
                    return new Invigorate("Invigorate 1", 3);
                case Options.Two:
                    return new Invigorate("Invigorate 2", 5);
                case Options.Three:
                    return new Invigorate("Invigorate 3", 7);
            }
            return null;
        }

        public override void Activate (Character character) {
            character.CurrentSP += Amount;
        }

        public enum Options {
            One,
            Two,
            Three
        }
    }
}