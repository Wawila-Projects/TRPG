using Assets.CharacterSystem.PassiveSkills.BuffEffects;

namespace Assets.CharacterSystem.PassiveSkills.StartSkills {
    public class AutoSakukaja : PassiveSkillsBase {
        public override string Name { get; protected set; }
        public override string Description  => "Automatic Sakukaja at the start of battle.";
        public override Phase ActivationPhase => Phase.Start;
        private AgilityEffect effect;
        public AutoSakukaja () {
            Name = "Auto-Sakukaja";
            effect = AgilityEffect.GetAgilityEffect(true);
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