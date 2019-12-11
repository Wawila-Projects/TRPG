using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using static Assets.CharacterSystem.PassiveSkills.PassiveSkillsBase;

namespace Assets.CharacterSystem.PassiveSkills {
    public class PassiveSkillController: MonoBehaviour {
        public Character Character {get; private set;}
        public IList<PassiveSkillsBase> PassiveSkills  {get; private set;} = new List<PassiveSkillsBase>();

        private IEnumerable<PassiveSkillsBase> _startPassiveSkills; 
        private IEnumerable<PassiveSkillsBase> _turnPassiveSkills; 
        private IEnumerable<PassiveSkillsBase> _endPassiveSkills; 

        public void AddSkill(PassiveSkillsBase skill) {
            PassiveSkills.Add(skill);
        }
        public void RemoveSkill(PassiveSkillsBase skill) {
            PassiveSkills.Remove(skill);
        }
        public void SetUp(Character character, IList<PassiveSkillsBase> skills) {
            Character = character;
            PassiveSkills = skills;

            _startPassiveSkills = PassiveSkills.Where(
                (s) => s.ActivationPhase == Phase.Start
            );

            _turnPassiveSkills = PassiveSkills.Where(
                (s) => s.ActivationPhase == Phase.Turn
            );

            _endPassiveSkills = PassiveSkills.Where(
                (s) => s.ActivationPhase == Phase.End
            );
        }

        public void HandleStartSkills(bool activate) => HandleSkill(_startPassiveSkills, activate);

        public void HandleTurnSkills(bool activate) => HandleSkill(_turnPassiveSkills, activate);

        public void HandleEndSkills(bool activate) => HandleSkill(_endPassiveSkills, activate);
        

        private void HandleSkill(IEnumerable<PassiveSkillsBase> skills, bool activate) {
            foreach (var skill in skills)
            {
                if (activate) {
                    skill.Activate(Character);
                    return;
                }
                skill.Terminate(Character);
            }
        }

    }
}