
using UnityEngine;

namespace Assets.CharacterSystem.PassiveSkills.RecoverySkills {
    public class DivineGrace : PassiveSkillsBase {
        public override string Name { get; protected set; }
        public override string Description => "Effects of healing magic are increased by 50%.";
        public override Phase ActivationPhase => Phase.Start;
        
        public DivineGrace () {
            Name = "Divine Grace";
            IsActive = true;
        }

        public override void Activate (Character character) {
            character.Persona.DivineGrace = true;
        }

        public override void Terminate (Character character) {
            character.Persona.DivineGrace = false;
        }

    }
}