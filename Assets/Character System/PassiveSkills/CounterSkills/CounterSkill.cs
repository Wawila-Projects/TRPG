
namespace Assets.CharacterSystem.PassiveSkills.CounterSkills {
    public class CounterSkill : PassiveSkillsBase {
        public float CounterAmount { get; }
        public override string Name { get; protected set; }
        public override string Description => $"{CounterAmount*100}% chance of reflecting Physical attacks.";
        public override Phase ActivationPhase => Phase.Start;

        private CounterSkill (string name, float amount) {
            Name = name;
            CounterAmount = amount;
        }

        public static CounterSkill GetCounterSkill (Options option) {
            switch (option)
            {
                case Options.Counter:
                    return new CounterSkill("Counter", 0.1f);
                case Options.Counterstrike:
                    return new CounterSkill("Counterstrike", 0.15f);
                case Options.HighCounter:
                    return new CounterSkill("High Counter", 0.2f);
            }

            return null;
        }

        public override void Activate (Character character) {
            if (IsActive) return;
            IsActive = true;
            character.Persona.CounterChance = CounterAmount;
        }
        public override void Terminate (Character character) {
            if (!IsActive) return;
            IsActive = false;
            character.Persona.CounterChance = 0f;
        }

        public enum Options {
            Counter,
            Counterstrike,
            HighCounter
        }
    }
}