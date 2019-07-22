using System.Collections.Generic;

namespace Assets.CharacterSystem.PassiveSkills.OffensiveSkills {
    public class AptPupil : PassiveSkillsBase {
        public override string Name { get; protected set; }
        public override string Description => "Increase critical rate.";
        public override Phase ActivationPhase => Phase.Start;

        public AptPupil () {
            Name = "Apt Pupil";
        }

        public override void Activate (Character character) {
            if (IsActive) return;
            IsActive = true;
            character.Persona.AptPupil = true;
        }

        public override void Terminate (Character character) {
            if (!IsActive) return;
            IsActive = false;
            character.Persona.AptPupil = false;
        }
    }
}