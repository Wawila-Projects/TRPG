using System;
using Assets.Enums;

namespace Assets.CharacterSystem.PassiveSkills.OffensiveSkills {
    public class ElementalBoost : PassiveSkillsBase {

        public Elements Element { get; }
        public float BoostAmount { get; }
        public override string Name { get; protected set; }
        public override string Description => $"Strengthens {Element} by {(BoostAmount-1)*100}%";
        public override Phase ActivationPhase => Phase.Start;

        private ElementalBoost (Elements element, float amount, string suffix) : base () {
            if (element == Elements.None || element == Elements.Recovery) {
                throw new ArgumentException ($"Invalid Element {element}");
            }

            Element = element;
            BoostAmount = amount;
            Name = $"{element} {suffix}";
        }

        public static ElementalBoost GetElementalBoost (Elements element, Options option) {
            var amount = option == Options.Amp ? 1.5f : 1.25f;
            try {
                return new ElementalBoost(element, amount, option.ToString());
            } catch (ArgumentException) {
                return null;
            }
        }

        public enum Options {
            Boost, Amp
        }

        public override void Activate (Character character) {
            if (IsActive) return;
            IsActive = true;
            character.Persona.ElementDamageModifier[Element] *= BoostAmount;
        }

        public override void Terminate (Character character) {
            if (!IsActive) return;
            IsActive = false;
            character.Persona.ElementDamageModifier[Element] /= BoostAmount;
        }
    }
}