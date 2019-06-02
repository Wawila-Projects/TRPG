using System;
using System.Linq;
using System.Collections.Generic;
using Assets.Enums;
using Assets.Personas;

namespace PeronsaFusionSystem {
    public class PersonaFusion {

        public IDictionary<Arcana, IList<PersonaBase>> PersonaLexicon;
        public IDictionary<(Arcana, Arcana), Arcana> FusionChart;

        public PersonaBase this[PersonaBase first, PersonaBase second] => GetPersona(first, second);


        public PersonaFusion() {
            SetFusionChart ();
        }

        //TODO Same Arcana persona downgrade, check for same persona, invalid fusions and debug
        public PersonaBase GetPersona(PersonaBase first, PersonaBase second) {
            if (first is null || second is null)
                throw new ArgumentNullException();
            
            if (first == second) 
                return null;

            (Arcana, Arcana) arcanaPair;
            if (first.Arcana < second.Arcana) {
                arcanaPair = (first.Arcana, second.Arcana);
            } else {
                arcanaPair = (second.Arcana, first.Arcana);
            }

            var lookupLevel = ((first.Level + second.Level)/2) + 1;
            var resultArcana = FusionChart[arcanaPair];
            var arcanaPersonas = PersonaLexicon[resultArcana];

            if (lookupLevel <= arcanaPersonas.First().Level && 
                !samePersonaCheck(first, second, arcanaPersonas.First())) {
                return arcanaPersonas.First();
            }

            if (lookupLevel >= arcanaPersonas.Last().Level && 
                !samePersonaCheck(first, second, arcanaPersonas.Last())) {
                return arcanaPersonas.First();
            }

            var personaResult = arcanaPersonas.Where(
                p => p.Level >= lookupLevel
            ).FirstOrDefault();

            return personaResult;

            bool samePersonaCheck(params PersonaBase [] personas) {
                return personas.Length == personas.Distinct().Count();
            }
        }

        private void SetFusionChart() {
            FusionChart = new Dictionary<(Arcana, Arcana), Arcana> () {
                // Fool
                {(Arcana.Fool, Arcana.Fool), Arcana.Fool},
                {(Arcana.Fool, Arcana.Magician), Arcana.Death},
                {(Arcana.Fool, Arcana.Priestess), Arcana.Moon},
                {(Arcana.Fool, Arcana.Empress), Arcana.HangedMan},
                {(Arcana.Fool, Arcana.Emperor), Arcana.Temperance},
                {(Arcana.Fool, Arcana.Hierophant), Arcana.Hermit},
                {(Arcana.Fool, Arcana.Lovers), Arcana.Chariot},
                {(Arcana.Fool, Arcana.Chariot), Arcana.Moon},
                {(Arcana.Fool, Arcana.Justice), Arcana.Star},
                {(Arcana.Fool, Arcana.Hermit), Arcana.Priestess},
                {(Arcana.Fool, Arcana.Fortune), Arcana.Lovers},
                {(Arcana.Fool, Arcana.Strength), Arcana.Death},
                {(Arcana.Fool, Arcana.HangedMan), Arcana.Tower},
                {(Arcana.Fool, Arcana.Death), Arcana.Strength},
                {(Arcana.Fool, Arcana.Temperance), Arcana.Hierophant},
                {(Arcana.Fool, Arcana.Devil), Arcana.Temperance},
                {(Arcana.Fool, Arcana.Tower), Arcana.Empress},
                {(Arcana.Fool, Arcana.Star), Arcana.Magician},
                {(Arcana.Fool, Arcana.Moon), Arcana.Justice},
                {(Arcana.Fool, Arcana.Sun), Arcana.Justice},
                {(Arcana.Fool, Arcana.Judgement), Arcana.Sun},

                //Magician
                {(Arcana.Magician, Arcana.Magician), Arcana.Magician},
                {(Arcana.Magician, Arcana.Priestess), Arcana.Temperance},
                {(Arcana.Magician, Arcana.Empress), Arcana.Justice},
                {(Arcana.Magician, Arcana.Emperor), Arcana.HangedMan},
                {(Arcana.Magician, Arcana.Hierophant), Arcana.Death},
                {(Arcana.Magician, Arcana.Lovers), Arcana.Devil},
                {(Arcana.Magician, Arcana.Chariot), Arcana.Priestess},
                {(Arcana.Magician, Arcana.Justice), Arcana.Emperor},
                {(Arcana.Magician, Arcana.Hermit), Arcana.Lovers},
                {(Arcana.Magician, Arcana.Fortune), Arcana.Justice},
                {(Arcana.Magician, Arcana.Strength), Arcana.Fool},
                {(Arcana.Magician, Arcana.HangedMan), Arcana.Empress},
                {(Arcana.Magician, Arcana.Death), Arcana.Hermit},
                {(Arcana.Magician, Arcana.Temperance), Arcana.Chariot},
                {(Arcana.Magician, Arcana.Devil), Arcana.Hierophant},
                {(Arcana.Magician, Arcana.Tower), Arcana.Temperance},
                {(Arcana.Magician, Arcana.Star), Arcana.Priestess},
                {(Arcana.Magician, Arcana.Moon), Arcana.Lovers},
                {(Arcana.Magician, Arcana.Sun), Arcana.Hierophant},
                {(Arcana.Magician, Arcana.Judgement), Arcana.Strength},

                //Priestess
                {(Arcana.Priestess, Arcana.Priestess), Arcana.Priestess},
                {(Arcana.Priestess, Arcana.Empress), Arcana.Emperor},
                {(Arcana.Priestess, Arcana.Emperor), Arcana.Empress},
                {(Arcana.Priestess, Arcana.Hierophant), Arcana.Magician},
                {(Arcana.Priestess, Arcana.Lovers), Arcana.Fortune},
                {(Arcana.Priestess, Arcana.Chariot), Arcana.Hierophant},
                {(Arcana.Priestess, Arcana.Justice), Arcana.Death},
                {(Arcana.Priestess, Arcana.Hermit), Arcana.Temperance},
                {(Arcana.Priestess, Arcana.Fortune), Arcana.Magician},
                {(Arcana.Priestess, Arcana.Strength), Arcana.Devil},
                {(Arcana.Priestess, Arcana.HangedMan), Arcana.Death},
                {(Arcana.Priestess, Arcana.Death), Arcana.Magician},
                {(Arcana.Priestess, Arcana.Temperance), Arcana.Devil},
                {(Arcana.Priestess, Arcana.Devil), Arcana.Moon},
                {(Arcana.Priestess, Arcana.Tower), Arcana.HangedMan},
                {(Arcana.Priestess, Arcana.Star), Arcana.Hermit},
                {(Arcana.Priestess, Arcana.Moon), Arcana.Hierophant},
                {(Arcana.Priestess, Arcana.Sun), Arcana.Chariot},
                {(Arcana.Priestess, Arcana.Judgement), Arcana.Justice},
                
                //Empress
                {(Arcana.Empress, Arcana.Empress), Arcana.Empress},
                {(Arcana.Empress, Arcana.Emperor), Arcana.Justice},
                {(Arcana.Empress, Arcana.Hierophant), Arcana.Fool},
                {(Arcana.Empress, Arcana.Lovers), Arcana.Judgement},
                {(Arcana.Empress, Arcana.Chariot), Arcana.Star},
                {(Arcana.Empress, Arcana.Justice), Arcana.Lovers},
                {(Arcana.Empress, Arcana.Hermit), Arcana.Strength},
                {(Arcana.Empress, Arcana.Fortune), Arcana.Hermit},
                {(Arcana.Empress, Arcana.Strength), Arcana.Chariot},
                {(Arcana.Empress, Arcana.HangedMan), Arcana.Priestess},
                {(Arcana.Empress, Arcana.Death), Arcana.Fool},
                {(Arcana.Empress, Arcana.Temperance), Arcana.Priestess},
                {(Arcana.Empress, Arcana.Devil), Arcana.Sun},
                {(Arcana.Empress, Arcana.Tower), Arcana.Emperor},
                {(Arcana.Empress, Arcana.Star), Arcana.Lovers},
                {(Arcana.Empress, Arcana.Moon), Arcana.Fortune},
                {(Arcana.Empress, Arcana.Sun), Arcana.Tower},
                {(Arcana.Empress, Arcana.Judgement), Arcana.Emperor},

                //Emperor
                {(Arcana.Emperor, Arcana.Emperor), Arcana.Emperor},
                {(Arcana.Emperor, Arcana.Hierophant), Arcana.Fortune},
                {(Arcana.Emperor, Arcana.Lovers), Arcana.Fool},
                {(Arcana.Emperor, Arcana.Chariot), Arcana.Strength},
                {(Arcana.Emperor, Arcana.Justice), Arcana.Chariot},
                {(Arcana.Emperor, Arcana.Hermit), Arcana.Hierophant},
                {(Arcana.Emperor, Arcana.Fortune), Arcana.Sun},
                {(Arcana.Emperor, Arcana.Strength), Arcana.Tower},
                {(Arcana.Emperor, Arcana.HangedMan), Arcana.Devil},
                {(Arcana.Emperor, Arcana.Death), Arcana.Hermit},
                {(Arcana.Emperor, Arcana.Temperance), Arcana.Devil},
                {(Arcana.Emperor, Arcana.Devil), Arcana.Justice},
                {(Arcana.Emperor, Arcana.Tower), Arcana.Star},
                {(Arcana.Emperor, Arcana.Star), Arcana.Lovers},
                {(Arcana.Emperor, Arcana.Moon), Arcana.Tower},
                {(Arcana.Emperor, Arcana.Sun), Arcana.Judgement},
                {(Arcana.Emperor, Arcana.Judgement), Arcana.Priestess},

                //Hierophant
                {(Arcana.Hierophant, Arcana.Hierophant), Arcana.Hierophant},
                {(Arcana.Hierophant, Arcana.Lovers), Arcana.Strength},
                {(Arcana.Hierophant, Arcana.Chariot), Arcana.Star},
                {(Arcana.Hierophant, Arcana.Justice), Arcana.HangedMan},
                {(Arcana.Hierophant, Arcana.Hermit), Arcana.Fortune},
                {(Arcana.Hierophant, Arcana.Fortune), Arcana.Justice},
                {(Arcana.Hierophant, Arcana.Strength), Arcana.Fool},
                {(Arcana.Hierophant, Arcana.HangedMan), Arcana.Sun},
                {(Arcana.Hierophant, Arcana.Death), Arcana.Chariot},
                {(Arcana.Hierophant, Arcana.Temperance), Arcana.Death},
                {(Arcana.Hierophant, Arcana.Devil), Arcana.HangedMan},
                {(Arcana.Hierophant, Arcana.Tower), Arcana.Judgement},
                {(Arcana.Hierophant, Arcana.Star), Arcana.Tower},
                {(Arcana.Hierophant, Arcana.Moon), Arcana.Priestess},
                {(Arcana.Hierophant, Arcana.Sun), Arcana.Lovers},
                {(Arcana.Hierophant, Arcana.Judgement), Arcana.Empress},

                //Lovers
                {(Arcana.Lovers, Arcana.Lovers), Arcana.Lovers},
                {(Arcana.Lovers, Arcana.Chariot), Arcana.Temperance},
                {(Arcana.Lovers, Arcana.Justice), Arcana.Judgement},
                {(Arcana.Lovers, Arcana.Hermit), Arcana.Chariot},
                {(Arcana.Lovers, Arcana.Fortune), Arcana.Strength},
                {(Arcana.Lovers, Arcana.Strength), Arcana.Death},
                {(Arcana.Lovers, Arcana.HangedMan), Arcana.Sun},
                {(Arcana.Lovers, Arcana.Death), Arcana.Temperance},
                {(Arcana.Lovers, Arcana.Temperance), Arcana.Strength},
                {(Arcana.Lovers, Arcana.Devil), Arcana.Moon},
                {(Arcana.Lovers, Arcana.Tower), Arcana.Empress},
                {(Arcana.Lovers, Arcana.Star), Arcana.Chariot},
                {(Arcana.Lovers, Arcana.Moon), Arcana.Magician},
                {(Arcana.Lovers, Arcana.Sun), Arcana.Empress},
                {(Arcana.Lovers, Arcana.Judgement), Arcana.HangedMan},

                //Chariot
                {(Arcana.Chariot, Arcana.Chariot), Arcana.Chariot},
                {(Arcana.Chariot, Arcana.Justice), Arcana.Moon},
                {(Arcana.Chariot, Arcana.Hermit), Arcana.Devil},
                {(Arcana.Chariot, Arcana.Fortune), Arcana.Priestess},
                {(Arcana.Chariot, Arcana.Strength), Arcana.Hermit},
                {(Arcana.Chariot, Arcana.HangedMan), Arcana.Fool},
                {(Arcana.Chariot, Arcana.Death), Arcana.Devil},
                {(Arcana.Chariot, Arcana.Temperance), Arcana.Strength},
                {(Arcana.Chariot, Arcana.Devil), Arcana.Temperance},
                {(Arcana.Chariot, Arcana.Tower), Arcana.Fortune},
                {(Arcana.Chariot, Arcana.Star), Arcana.Moon},
                {(Arcana.Chariot, Arcana.Moon), Arcana.Lovers},
                {(Arcana.Chariot, Arcana.Sun), Arcana.Priestess},
                {(Arcana.Chariot, Arcana.Judgement), Arcana.Hierophant},

                //Justice
                {(Arcana.Justice, Arcana.Justice), Arcana.Justice},
                {(Arcana.Justice, Arcana.Hermit), Arcana.Magician},
                {(Arcana.Justice, Arcana.Fortune), Arcana.Emperor},
                {(Arcana.Justice, Arcana.Strength), Arcana.Hierophant},
                {(Arcana.Justice, Arcana.HangedMan), Arcana.Lovers},
                {(Arcana.Justice, Arcana.Death), Arcana.Fool},
                {(Arcana.Justice, Arcana.Temperance), Arcana.Emperor},
                {(Arcana.Justice, Arcana.Devil), Arcana.Fool},
                {(Arcana.Justice, Arcana.Tower), Arcana.Sun},
                {(Arcana.Justice, Arcana.Star), Arcana.Empress},
                {(Arcana.Justice, Arcana.Moon), Arcana.Devil},
                {(Arcana.Justice, Arcana.Sun), Arcana.HangedMan},
                {(Arcana.Justice, Arcana.Judgement), Arcana.Tower},

                //Hermit
                {(Arcana.Hermit, Arcana.Hermit), Arcana.Hermit},
                {(Arcana.Hermit, Arcana.Fortune), Arcana.Star},
                {(Arcana.Hermit, Arcana.Strength), Arcana.Hierophant},
                {(Arcana.Hermit, Arcana.HangedMan), Arcana.Star},
                {(Arcana.Hermit, Arcana.Death), Arcana.Strength},
                {(Arcana.Hermit, Arcana.Temperance), Arcana.Strength},
                {(Arcana.Hermit, Arcana.Devil), Arcana.Priestess},
                {(Arcana.Hermit, Arcana.Tower), Arcana.Judgement},
                {(Arcana.Hermit, Arcana.Star), Arcana.Strength},
                {(Arcana.Hermit, Arcana.Moon), Arcana.Priestess},
                {(Arcana.Hermit, Arcana.Sun), Arcana.Devil},
                {(Arcana.Hermit, Arcana.Judgement), Arcana.Emperor},

                //Fortune
                {(Arcana.Fortune, Arcana.Fortune), Arcana.Fortune},
                {(Arcana.Fortune, Arcana.Strength), Arcana.Temperance},
                {(Arcana.Fortune, Arcana.HangedMan), Arcana.Emperor},
                {(Arcana.Fortune, Arcana.Death), Arcana.Star},
                {(Arcana.Fortune, Arcana.Temperance), Arcana.Empress},
                {(Arcana.Fortune, Arcana.Devil), Arcana.Hierophant},
                {(Arcana.Fortune, Arcana.Tower), Arcana.HangedMan},
                {(Arcana.Fortune, Arcana.Star), Arcana.Devil},
                {(Arcana.Fortune, Arcana.Moon), Arcana.Sun},
                {(Arcana.Fortune, Arcana.Sun), Arcana.Star},
                {(Arcana.Fortune, Arcana.Judgement), Arcana.Tower},

                //Strength
                {(Arcana.Strength, Arcana.Strength), Arcana.Strength},
                {(Arcana.Strength, Arcana.HangedMan), Arcana.Temperance},
                {(Arcana.Strength, Arcana.Death), Arcana.Hierophant},
                {(Arcana.Strength, Arcana.Temperance), Arcana.Chariot},
                {(Arcana.Strength, Arcana.Devil), Arcana.Death},
                {(Arcana.Strength, Arcana.Tower), Arcana.Chariot},
                {(Arcana.Strength, Arcana.Star), Arcana.Moon},
                {(Arcana.Strength, Arcana.Moon), Arcana.Magician},
                {(Arcana.Strength, Arcana.Sun), Arcana.Moon},
                {(Arcana.Strength, Arcana.Judgement), Arcana.Fortune},

                //Hanged Man
                {(Arcana.HangedMan, Arcana.HangedMan), Arcana.HangedMan},
                {(Arcana.HangedMan, Arcana.Death), Arcana.Moon},
                {(Arcana.HangedMan, Arcana.Temperance), Arcana.Death},
                {(Arcana.HangedMan, Arcana.Devil), Arcana.Fortune},
                {(Arcana.HangedMan, Arcana.Tower), Arcana.Hermit},
                {(Arcana.HangedMan, Arcana.Star), Arcana.Justice},
                {(Arcana.HangedMan, Arcana.Moon), Arcana.Strength},
                {(Arcana.HangedMan, Arcana.Sun), Arcana.Hierophant},
                {(Arcana.HangedMan, Arcana.Judgement), Arcana.Star},

                //Death
                {(Arcana.Death, Arcana.Death), Arcana.Death},
                {(Arcana.Death, Arcana.Temperance), Arcana.HangedMan},
                {(Arcana.Death, Arcana.Devil), Arcana.Chariot},
                {(Arcana.Death, Arcana.Tower), Arcana.Sun},
                {(Arcana.Death, Arcana.Star), Arcana.Devil},
                {(Arcana.Death, Arcana.Moon), Arcana.Hierophant},
                {(Arcana.Death, Arcana.Sun), Arcana.Priestess},
                {(Arcana.Death, Arcana.Judgement), Arcana.Magician},

                //Temperance
                {(Arcana.Temperance, Arcana.Temperance), Arcana.Temperance},
                {(Arcana.Temperance, Arcana.Devil), Arcana.Fool},
                {(Arcana.Temperance, Arcana.Tower), Arcana.Fortune},
                {(Arcana.Temperance, Arcana.Star), Arcana.Sun},
                {(Arcana.Temperance, Arcana.Moon), Arcana.Fortune},
                {(Arcana.Temperance, Arcana.Sun), Arcana.Magician},
                {(Arcana.Temperance, Arcana.Judgement), Arcana.Hermit},

                //Devil
                {(Arcana.Devil, Arcana.Devil), Arcana.Devil},
                {(Arcana.Devil, Arcana.Tower), Arcana.Magician},
                {(Arcana.Devil, Arcana.Star), Arcana.Strength},
                {(Arcana.Devil, Arcana.Moon), Arcana.Chariot},
                {(Arcana.Devil, Arcana.Sun), Arcana.Hermit},
                {(Arcana.Devil, Arcana.Judgement), Arcana.Lovers},

                //Tower
                {(Arcana.Tower, Arcana.Tower), Arcana.Tower},
                {(Arcana.Tower, Arcana.Star), Arcana.Death},
                {(Arcana.Tower, Arcana.Moon), Arcana.Hermit},
                {(Arcana.Tower, Arcana.Sun), Arcana.Emperor},
                {(Arcana.Tower, Arcana.Judgement), Arcana.Moon},

                //Star
                {(Arcana.Star, Arcana.Star), Arcana.Star},
                {(Arcana.Star, Arcana.Moon), Arcana.Temperance},
                {(Arcana.Star, Arcana.Sun), Arcana.Judgement},
                {(Arcana.Star, Arcana.Judgement), Arcana.Fortune},

                //Moon
                {(Arcana.Moon, Arcana.Moon), Arcana.Moon},
                {(Arcana.Moon, Arcana.Sun), Arcana.Empress},
                {(Arcana.Moon, Arcana.Judgement), Arcana.Fool},

                //Sun
                {(Arcana.Sun, Arcana.Sun), Arcana.Sun},
                {(Arcana.Sun, Arcana.Judgement), Arcana.Death},

                //Judgement
                {(Arcana.Judgement, Arcana.Judgement), Arcana.Judgement},
            };
        }

    }
}