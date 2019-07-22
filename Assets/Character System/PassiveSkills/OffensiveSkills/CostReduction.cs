namespace Assets.CharacterSystem.PassiveSkills.OffensiveSkills {
    public class CostReduction : PassiveSkillsBase {
        public override string Name { get; protected set; }

        public override string Description { get; }

        public override Phase ActivationPhase => Phase.Start;

        public bool IsPhysical { get; }

        public CostReduction (bool isPhysical) {
            IsPhysical = isPhysical;
            if (isPhysical) {
                Name = "Arms Master";
                Description = "Half HP cost for physical skills.";
                return;
            }

            Name = "Spell Master";
            Description = "Half SP cost for magic skills.";
        }

        public override void Activate (Character character) {
            if (IsActive) return;
            IsActive = true;

            if (IsPhysical) {
                character.Persona.ArmsMaster = true;
                return;
            }
            character.Persona.SpellMaster = true;
        }

        public override void Terminate (Character character) {
            if (!IsActive) return;
            IsActive = false;

            if (IsPhysical) {
                character.Persona.ArmsMaster = false;
                return;
            }
            character.Persona.SpellMaster = false;
        }
    }
}