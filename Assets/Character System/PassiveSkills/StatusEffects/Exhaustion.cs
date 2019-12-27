using System;

namespace Assets.CharacterSystem.PassiveSkills.StatusEffects {
    public class Exhaustion : StatusEffect {
        public Exhaustion () : base (true) { }
        protected override void Effect (Character character) {
            character.CurrentSP -= (int) Math.Round ((character.Sp * 0.06f));
            //ToDo: Damage taken increased by 50%
        }

        protected override bool ShouldTerminate (Character character) {
            return character.CurrentHP <= (character.Sp * 0.1);
        }
    }
}