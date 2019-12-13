using UnityEngine;

namespace Assets.CharacterSystem.PassiveSkills.EndSkills {
    public class LifeAid : PassiveSkillsBase {
        public override string Name { get; protected set; }
        public override string Description => "Recover 8% HP and SP after a victory.";
        public override Phase ActivationPhase => Phase.End;

        public LifeAid () {
            Name = "Life Aid";
        }

        public override void Activate (Character character) {
            if (IsActive) return;
            IsActive = true;

            character.CurrentHP += Mathf.RoundToInt(character.Hp * 0.08f);
            character.CurrentSP += Mathf.RoundToInt(character.Sp * 0.08f);
        }
    }
}