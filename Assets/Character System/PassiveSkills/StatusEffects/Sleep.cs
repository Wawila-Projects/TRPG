using System;

namespace Assets.CharacterSystem.PassiveSkills.StatusEffects {
    public class Sleep : StatusEffect {
        public Sleep () : base (true) { }
        protected override void Effect (Character character) {
            //TODO Evasion = 0%
            character.CurrentSP += 5;
            character.CurrentHP += (int) Math.Round(character.Hp * 0.04);
            character.TurnFinished = true;
        }

        protected override bool ShouldTerminate (Character character) {
            return false;
        }
    }
}