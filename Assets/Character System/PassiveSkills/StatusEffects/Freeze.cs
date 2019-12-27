namespace Assets.CharacterSystem.PassiveSkills.StatusEffects {
    public class Freeze : StatusEffect {
        public Freeze () : base (true) { }
        protected override void Effect (Character character) {
            character.CurrentMovement = 0;
            //TODO: Evasion = 0%
        }
    }
}