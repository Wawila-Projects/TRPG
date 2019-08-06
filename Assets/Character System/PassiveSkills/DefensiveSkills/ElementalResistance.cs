
using System;
using Assets.Enums;

namespace Assets.CharacterSystem.PassiveSkills.DefensiveSkills {
    public class ElementalResistance : PassiveSkillsBase {

        public Elements Element { get; }
        public ResistanceModifiers Modifier { get; }
        public override string Name { get; protected set; }
        public override string Description => $"{Modifier}s {Element} attacks.";
        public override Phase ActivationPhase => Phase.Start;

        private ElementalResistance (Elements element, ResistanceModifiers modifier) : base () {
            if (element == Elements.None || element == Elements.Recovery) {
                throw new ArgumentException ($"Invalid Element {element}");
            }

            Element = element;
            Modifier = modifier;
            Name = $"{modifier} {element}";
        }

        public static ElementalResistance GetElementalResistance (Elements element, ResistanceModifiers modifier) {
            try {
                return new ElementalResistance(element, modifier);
            } catch (ArgumentException) {
                return null;
            }
        }

        public override void Activate (Character character) {
            if (IsActive) return;
            IsActive = character.Persona.ChangeResistances(Element, Modifier, buff: true);
        }

        public override void Terminate (Character character) {
            if (!IsActive) return;
            IsActive = false;
            character.Persona.ChangeResistances(Element, Modifier, clear: true);
        }
    }
}