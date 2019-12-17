namespace Assets.CharacterSystem.PassiveSkills.OffensiveSkills {
    public class MagicBoost : PassiveSkillsBase {
        public const float BoostAmount = 1.25f;
        public override string Name { get; protected set; }
        public override string Description => $"Strengthen all magical attacks by 25%.";
        public override Phase ActivationPhase => Phase.Start;

        private MagicBoost () {
            Name = $"Magic Boost";
        }

        public override void Activate (Character character) {
            if (character.PassiveSkills.HasSkill(this)) return;
            if (IsActive) return;
            IsActive = true;

            var modifierDict = character.Persona.ElementDamageModifier;
            foreach(var key in modifierDict.Keys) {
                modifierDict[key] *= BoostAmount;
            }
        }

        public override void Terminate (Character character) {
            if (!IsActive) return;

            var modifierDict = character.Persona.ElementDamageModifier;
            foreach(var key in modifierDict.Keys) {
                modifierDict[key] /= BoostAmount;
            }

            base.Terminate(character);
        }
    }
}