namespace Assets.CharacterSystem.PassiveSkills.StatusEffects {
    public class Rage : StatusEffect {
        public Rage () : base (true) { }
        protected override void Effect (Character character) {
            //TODO Evasion = 50%
            //TODO Hit = 50%
            //TODO Attack = 150%
            //TODO Auto Attack
        }

        protected override bool ShouldTerminate (Character character) {
            return false;
        }
    }
}