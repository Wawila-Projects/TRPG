namespace Assets.CharacterSystem.PassiveSkills.EndSkills {
    public class VictoryCry : PassiveSkillsBase {
        public override string Name { get; protected set; }
        public override string Description => "Fully recover HP and SP after a victory.";
        public override Phase ActivationPhase => Phase.End;

        public VictoryCry () {
            Name = "Victory Cry";
        }

        public override void Activate (Character character) {
            if (IsActive) return;
            IsActive = true;

            character.CurrentHP = character.Hp;
            character.CurrentSP = character.Sp;
        }
    }
}