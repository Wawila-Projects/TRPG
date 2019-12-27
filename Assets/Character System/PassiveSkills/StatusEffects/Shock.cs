using System;

namespace Assets.CharacterSystem.PassiveSkills.StatusEffects {
    public class Shock : StatusEffect {
        public Shock () : base (true) { }
        protected override void Effect (Character character) {
            //TODO: Critical Evasion = 0% by physical spells
        }
    }
}