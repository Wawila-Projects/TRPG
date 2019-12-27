//TODO: Add ability to remove at any trigger. 
using Assets.UI;

namespace Assets.CharacterSystem.PassiveSkills {
    public abstract class StatusEffect : PassiveSkillsBase {
        public sealed override Phase ActivationPhase => Phase.Turn;
        public sealed override string Name { get; protected set; }
        public sealed override string Description => "";

        public bool ActivateImmediately { get; protected set; }
        private int _turnsActive;

        public override string ToString() => $"{Name} | {ActivationPhase.ToString()} | {_turnsActive}";

        protected StatusEffect (bool activateImmediately) {
            ActivateImmediately = activateImmediately;
            if (ActivateImmediately) {
                _turnsActive = -1;
            }
        }

        protected abstract void Effect (Character character);

        protected virtual bool ShouldTerminate(Character character) {
            return _turnsActive == 3;
        }

        public sealed override void Activate (Character character) {
            IsActive = true;
            if (ShouldTerminate(character)) {
                Terminate (character);
                return;
            }
            UIFloatingText.Create(Name, character.gameObject, Enums.Elements.Ailment);
            Effect (character);
            ++_turnsActive;
        }

        public sealed override void Terminate (Character character) {
            base.Terminate (character);
        }
    }
}