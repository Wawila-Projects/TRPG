using Assets.Enums;

namespace Assets.CharacterSystem.PassiveSkills.BuffEffects {
    public class AgilityEffect : BuffEffect {
        public override string Name { get; protected set; }
        public override string Description { get; }

        public AgilityEffect (bool buff): base(buff) {
            if (buff) {
                Name = "Tarukaja";
                Description = "Increases 1 ally's Agility for 3 turns";
            } else {
                Name = "Tarunda";
                Description = "Decreases 1 foe's Agility for 3 turns";
            }
        }

        protected override void Effect (Character character) {
            var currentModifier = character.Persona.EvadeBuff;

            if (Buff) {
                if (currentModifier == StatsModifiers.Debuff) {
                    Terminate (character);
                    return;
                }
                character.Persona.EvadeBuff = StatsModifiers.Buff;
                character.Persona.HitBuff = StatsModifiers.Buff;
                return;
            }

            if (currentModifier == StatsModifiers.Buff) {
                Terminate (character);
                return;
            }

            character.Persona.EvadeBuff = StatsModifiers.Debuff;
            character.Persona.HitBuff = StatsModifiers.Debuff;
        }

        public override void Terminate (Character character) {
            if (!IsActive) return;
            character.Persona.EvadeBuff = StatsModifiers.None;
            character.Persona.HitBuff = StatsModifiers.None;
            base.Terminate (character);
        }
    }
}