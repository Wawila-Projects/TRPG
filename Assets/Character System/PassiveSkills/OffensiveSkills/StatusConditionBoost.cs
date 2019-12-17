using System;
using Asstes.CharacterSystem.StatusEffects;

namespace Assets.CharacterSystem.PassiveSkills.OffensiveSkills {
    public class StatusConditionBoost : PassiveSkillsBase {
        public StatusCondition StatusCondition { get; }
        public const float BoostAmount = 1.5f;
        public override string Name { get; protected set; }
        public override string Description => $"Increases chance of inflicting {StatusCondition}";
        public override Phase ActivationPhase => Phase.Start;

        private StatusConditionBoost (StatusCondition condition) {
            if (condition == StatusCondition.None || condition == StatusCondition.Down) {
                throw new ArgumentException ($"Invalid Status Condition {condition}");
            }

            StatusCondition = condition;
            Name = $"{condition} Boost";
        }

        public static StatusConditionBoost GetStatusConditionBoost (StatusCondition condition) {
            try {
                return new StatusConditionBoost(condition);
            } catch(ArgumentException) {
                return null;
            }
        }

        public override void Activate (Character character) {
            if (character.PassiveSkills.HasSkill(this)) return;
            if (IsActive) return;
            IsActive = true;
            character.Persona.StatusConditionModifier[StatusCondition] *= BoostAmount;
        }

        public override void Terminate (Character character) {
            if (!IsActive) return;
            character.Persona.StatusConditionModifier[StatusCondition] /= BoostAmount;
            base.Terminate(character);
        }
    }
}