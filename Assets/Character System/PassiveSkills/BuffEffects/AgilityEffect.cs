using Assets.Enums;

namespace Assets.CharacterSystem.PassiveSkills.BuffEffects {
    public class AgilityEffect : PassiveSkillsBase {
        public bool Buff { get; }
        public override string Name { get; protected set; }
        public override string Description { get; }
        public override Phase ActivationPhase => Phase.Turn;
        private int turnsActive = 0;

        private AgilityEffect (string name, bool buff) {
            Name = name;
            Buff = buff;
            Description = buff ? "Increases 1 ally's Agility for 3 turns" :
                "Decreases 1 foe's Agility for 3 turns";
        }

        public static AgilityEffect GetAgilityEffect (bool buff) {
            return new AgilityEffect (buff ? "Tarukaja" : "Tarunda", buff);
        }

        public override void Activate (Character character) {
            if (IsActive) return;
            IsActive = true;

            var firstTime = turnsActive == 0;
            var currentModifier = character.Persona.EvadeBuff;

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
            base.Terminate(character);
        }
    }
}