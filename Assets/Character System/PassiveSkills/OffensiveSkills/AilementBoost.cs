namespace Assets.CharacterSystem.PassiveSkills.OffensiveSkills {
    public class AilementBoost : PassiveSkillsBase {
        public float BoostAmount => 1.5f;
        public override string Name { get; protected set; }
        public override string Description => $"Increase chance of inflicting all ailments.";
        public override Phase ActivationPhase => Phase.Start;

        private AilementBoost () {
            Name = $"Ailement Boost";
        }

        public override void Activate (Character character) {
            if (IsActive) return;
            IsActive = true;

            var modifierDict = character.Persona.StatusConditionModifier;
            foreach(var key in modifierDict.Keys) {
                modifierDict[key] *= BoostAmount;
            }
        }

        public override void Terminate (Character character) {
            if (!IsActive) return;
            IsActive = false;

            var modifierDict = character.Persona.StatusConditionModifier;
            foreach(var key in modifierDict.Keys) {
                modifierDict[key] /= BoostAmount;
            }
        }
    }
}