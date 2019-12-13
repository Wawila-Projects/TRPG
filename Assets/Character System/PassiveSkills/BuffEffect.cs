namespace Assets.CharacterSystem.PassiveSkills.BuffEffects {
    abstract public class BuffEffect : PassiveSkillsBase {
        public sealed override Phase ActivationPhase => Phase.Turn;
        public bool Buff { get; }
        protected int TurnsActive = -1;
        protected int TurnAmount;

        protected BuffEffect (bool buff, int turnAmount = 3) {
            Buff = buff;
            TurnAmount = 3;
        }

        protected abstract void Effect (Character character);

        public sealed override void Activate (Character character) {
            if (TriggerEffect (character)) 
                Effect (character);
        }

        protected bool TriggerEffect (Character character) {
            if (IsActive) return false;
            IsActive = true;

            var firstTime = TurnsActive == 0;
            ++TurnsActive;
            if (!firstTime) {
                if (TurnsActive == TurnAmount) {
                    Terminate (character);
                    return false;
                }
                return false;
            }
            return true;
        }
    }
}