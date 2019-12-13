using Assets.Enums;

namespace Assets.CharacterSystem.PassiveSkills.BuffEffects {
    public class AttackEffect : PassiveSkillsBase {
        public bool Buff { get; }
        public override string Name { get; protected set; }
        public override string Description { get; }
        public override Phase ActivationPhase => Phase.Turn;
        private int turnsActive = 0;

        private AttackEffect (string name, bool buff) {
            Name = name;
            Buff = buff;
            Description = buff ? "Increases 1 ally's Attack power for 3 turns" :
                "Decreases 1 foe's Attack power for 3 turns";
        }

        public static AttackEffect GetAttackEffect (bool buff) {
            return new AttackEffect (buff ? "Tarukaja" : "Tarunda", buff);
        }

        public override void Activate (Character character) {
            if (IsActive) return;
            IsActive = true;

            var firstTime = turnsActive == 0;
            var currentModifier = character.Persona.AttackBuff;

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
            base.Terminate(character);
        }
    }
}