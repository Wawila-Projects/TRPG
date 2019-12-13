using Assets.CharacterSystem.PassiveSkills.BuffEffects;
using UnityEngine;

namespace Assets.CharacterSystem.PassiveSkills.StartSkills {
    public class HeatUp : PassiveSkillsBase {
        public override string Name { get; protected set; }
        public override string Description => "Recover 5% HP and 10 SP at the start of preemptive turn.";
        public override Phase ActivationPhase => Phase.Start;

        public HeatUp () {
            Name = "Heat Up";
        }

        public override void Activate (Character character) {
            if (IsActive) return;
            IsActive = true;

            character.CurrentHP += Mathf.RoundToInt(character.Hp * 0.05f);
            character.CurrentSP += 10;
        }
    }
}