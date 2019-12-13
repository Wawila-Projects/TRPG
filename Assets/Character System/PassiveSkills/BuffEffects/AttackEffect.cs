using Assets.Enums;

namespace Assets.CharacterSystem.PassiveSkills.BuffEffects {
    public class AttackEffect : BuffEffect {
        public override string Name { get; protected set; }
        public override string Description { get; }
        public AttackEffect (bool buff): base(buff) {
            if (buff) {
                Name = "Tarukaja";
                Description = "Increases 1 ally's Attack power for 3 turns";
            } else {
                Name = "Tarunda";
                Description = "Decreases 1 foe's Attack power for 3 turns";
            }
        }

        protected override void Effect (Character character) {
            var currentModifier = character.Persona.AttackBuff;
            if (Buff) {
                if (currentModifier == StatsModifiers.Debuff) {
                    Terminate (character);
                    return;
                }
                character.Persona.AttackBuff = StatsModifiers.Buff;
                return;
            }

            if (currentModifier == StatsModifiers.Buff) {
                Terminate (character);
                return;
            }

            character.Persona.AttackBuff = StatsModifiers.Debuff;
        }
        public override void Terminate (Character character) {
            if (!IsActive) return;
            character.Persona.AttackBuff = StatsModifiers.None;
            character.PassiveSkills.RemoveSkill (this);
            base.Terminate (character);
        }
    }
}