using System;

namespace Assets.CharacterSystem.PassiveSkills.StatusEffects {
    public class Dizzy : StatusEffect {
        public Dizzy () : base (true) { }
        private bool turnLost = false;
        protected override void Effect (Character character) {
            character.TurnFinished = true;
            turnLost = true;
        }

        protected override bool ShouldTerminate (Character character) {
            return turnLost;
        }
    }
}