using Assets.Enums;

namespace Assets.CharacterSystem.PassiveSkills.BuffEffects {
    public class DefenceEffect : PassiveSkillsBase {
        public bool Buff { get; }
        public override string Name { get; protected set; }
        public override string Description { get; }
        public override Phase ActivationPhase => Phase.Turn;
        private int turnsActive = 0;

        private DefenceEffect (string name, bool buff) {
            Name = name;
            Buff = buff;
            Description = buff ? "Increases 1 ally's Defence for 3 turns" :
                "Decreases 1 foe's Defence for 3 turns";
        }

        public static DefenceEffect GetDefenceEffect (bool buff) {
            return new DefenceEffect (buff ? "Sakukaja" : "Sakunda", buff);
        }

        public override void Activate (Character character) {
            if (IsActive) return;
            IsActive = true;

            var firstTime = turnsActive == 0;
            var currentModifier = character.Persona.DefenceBuff;

            if (!firstTime) {
                if (turnsActive++ == 3) {
                    Terminate (character);
                }
                return;
            }

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
            base.Terminate(character);
        }
    }
}