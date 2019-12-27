using System;

namespace Assets.CharacterSystem.PassiveSkills.StatusEffects {
    public class Burn : StatusEffect {
        public Burn () : base (true) { }
        protected override void Effect (Character character) {
            character.CurrentHP -= (int) Math.Round ((character.Hp * 0.1f));
        }

        protected override bool ShouldTerminate (Character character) {
            return character.CurrentHP <= (character.Hp * 0.1);
        }
    }
}