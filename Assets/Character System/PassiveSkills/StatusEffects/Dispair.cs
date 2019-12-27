using System;

namespace Assets.CharacterSystem.PassiveSkills.StatusEffects {
    public class Dispair : StatusEffect {
        public Dispair () : base (false) { }

        private int turnCounter = 0;
        protected override void Effect (Character character) {
            character.CurrentSP -= (int) Math.Round ((character.Sp * 0.05f));
            if (turnCounter == 3) {
                character.CurrentHP = 0;
                return;
            }
            ++turnCounter;
        }
    }
}