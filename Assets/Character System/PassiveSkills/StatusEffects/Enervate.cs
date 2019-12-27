using System;

namespace Assets.CharacterSystem.PassiveSkills.StatusEffects {
    public class Enervation : StatusEffect {
        public Enervation () : base (true) { }
        protected override void Effect (Character character) {
            //TODO: Stats/2
            //TODO: Silenced
        }

        protected override bool ShouldTerminate (Character character) {
            return false;
        }

    }
}