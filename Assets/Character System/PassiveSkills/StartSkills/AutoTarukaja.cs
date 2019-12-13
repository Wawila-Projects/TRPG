using Assets.CharacterSystem.PassiveSkills.BuffEffects;

namespace Assets.CharacterSystem.PassiveSkills.StartSkills {
    public class AutoTarukaja : PassiveSkillsBase {
        public override string Name { get; protected set; }
        public override string Description => "Automatic Tarukaja at the start of battle.";
        public override Phase ActivationPhase => Phase.Start;
        private AttackEffect effect;
        public AutoTarukaja () {
            Name = "Auto-Tarukaja";
            effect = AttackEffect.GetAttackEffect(true);
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