using System;
using System.Collections.Generic;
using Assets.Enums;

namespace Assets.CharacterSystem.PassiveSkills.BuffEffects {
    public class BuffEffect : PassiveSkillsBase {
        public sealed override Phase ActivationPhase => Phase.Turn;
        public StatsModifiers Modifier { get; }
        public List<Statistics> Stats { get; }
        public override string Description => _description;
        public override string Name { get; protected set; }

        private int _turnsActive;
        private int _turnAmount = -1;
        private string _description;

        private BuffEffect (List<Statistics> stat, StatsModifiers modifier, int turnAmount = 3) {
            if (modifier == StatsModifiers.None) {
                throw new ArgumentException ($"Invalid Modifier {modifier}");
            }

            Stats = stat;
            Modifier = modifier;
            _turnAmount = 3;
            (Name, _description) = GetNameDescription ();
        }

        public static BuffEffect AttackEffect (bool buff, int turnAmount = 3) {
            return new BuffEffect (
                new List<Statistics> () {
                    Statistics.Magic, Statistics.Strength
                },
                buff ? StatsModifiers.Buff : StatsModifiers.Debuff,
                turnAmount
            );
        }
        public static BuffEffect DefenceEffect (bool buff, int turnAmount = 3) {
            return new BuffEffect (
                new List<Statistics> () {
                    Statistics.Endurance
                },
                buff ? StatsModifiers.Buff : StatsModifiers.Debuff,
                turnAmount
            );
        }
        public static BuffEffect AgilityEffect (bool buff, int turnAmount = 3) {
            return new BuffEffect (
                new List<Statistics> () {
                    Statistics.Agility
                },
                buff ? StatsModifiers.Buff : StatsModifiers.Debuff,
                turnAmount
            );
        }

        public sealed override void Activate (Character character) {
            bool wasApplied = false;
            foreach (var stat in Stats) {
                wasApplied = character.Persona.BuffStats (stat, Modifier);
            }
            if (!wasApplied || _turnsActive == _turnAmount) {
                Terminate (character);
            }
            ++_turnsActive;
        }

        public override void Terminate (Character character) {
            if (!IsActive) return;
            foreach (var stat in Stats) {
                character.Persona.BuffStats (stat, StatsModifiers.None);
            }
            base.Terminate (character);
        }

        private (string name, string description) GetNameDescription () {
            string name;
            string description;

            if (Stats.Contains (Statistics.Endurance)) {
                name = "Raku";
                description = "defence";
            } else if (Stats.Contains (Statistics.Agility)) {
                name = "Suku";
                description = "hit/evasion";
            } else {
                name = "Taru";
                description = "attack";
            }
            
            if (Modifier == StatsModifiers.Buff) {
                name += "kaja";
                description = $"Increase {description}";
            } else {
                name += "nda";
                description = $"Decrease {description}";
            }

            return (name, description);
        }
    }
}