using Assets.CharacterSystem.PassiveSkills.BuffEffects;

namespace Assets.CharacterSystem.PassiveSkills.StartSkills {
    public class AutoRakukaja : PassiveSkillsBase {
        public override string Name { get; protected set; }
        public override string Description  => "Automatic Rakukaja at the start of battle.";
        public override Phase ActivationPhase => Phase.Start;
        private DefenceEffect effect;
        public AutoRakukaja () {
            Name = "Auto-Rakukaja";
            effect = DefenceEffect.GetDefenceEffect(true);
        }

        public override void Activate (Character character) {
            if (IsActive) return;
            IsActive = true;
            character.PassiveSkills.AddSkill(effect);
            IsActive = true;
        }
        public override void Terminate (Character character) {
            if (!IsActive) return;
            effect.Terminate(character);
            base.Terminate(character);
        }
    }
}