using System;

namespace Assets.CharacterSystem.PassiveSkills.StatusEffects {
    public class Down : StatusEffect {
        public Down () : base (true) { }
        private bool turnDown = false;
        protected override void Effect (Character character) {
            character.CurrentMovement = 1;
            turnDown = true;
        }

        protected override bool ShouldTerminate (Character character) {
            return turnDown;
        }
    }
}