using Assets.Enums;

namespace Assets.CharacterSystem.PassiveSkills.BuffEffects {
    public class DefenceEffect : BuffEffect {
        public override string Name { get; protected set; }
        public override string Description { get; }
        private int turnsActive = 0;

        public DefenceEffect (bool buff): base(buff) {
            if (buff) {
                Name = "Sakukaja";
                Description = "Increases 1 ally's Defence for 3 turns";
            } else {
                Name = "Sakunda";
                Description = "Decreases 1 foe's Defence for 3 turns";
            }
        }

        protected override void Effect (Character character) {
            var currentModifier = character.Persona.DefenceBuff;
            if (Buff) {
                if (currentModifier == StatsModifiers.Debuff) {
                    Terminate (character);
                    return;
                }
                character.Persona.DefenceBuff = StatsModifiers.Buff;
                return;
            }

            if (currentModifier == StatsModifiers.Buff) {
                Terminate (character);
                return;
            }

            character.Persona.DefenceBuff = StatsModifiers.Debuff;
        }
        public override void Terminate (Character character) {
            if (!IsActive) return;
            character.Persona.DefenceBuff = StatsModifiers.None;
            base.Terminate (character);
        }
    }
}