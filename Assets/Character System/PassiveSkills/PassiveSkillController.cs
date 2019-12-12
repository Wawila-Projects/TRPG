using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Assets.CharacterSystem.PassiveSkills.PassiveSkillsBase;
using Assets.Spells;
using Assets.Utils;

namespace Assets.CharacterSystem.PassiveSkills {
    [RequireComponent (typeof (Character))]
    public class PassiveSkillController : MonoBehaviour {
        public Character Character;
        public IList<PassiveSkillsBase> PassiveSkills { get; private set; } = new List<PassiveSkillsBase> ();

        private IEnumerable<PassiveSkillsBase> _startPassiveSkills;
        private IEnumerable<PassiveSkillsBase> _turnPassiveSkills;
        private IEnumerable<PassiveSkillsBase> _endPassiveSkills;

        private void Awake () {
            if (Character == null) {
                Character = GetComponent<Character> ();
            }
        }

        private void Start () {
            SetUp (Character.Persona.SpellBook.Spells.ConvertTo<ISpell, PassiveSkillsBase> ());
        }

        public void AddSkill (PassiveSkillsBase skill) {
            PassiveSkills.Add (skill);
        }
        public void RemoveSkill (PassiveSkillsBase skill) {
            PassiveSkills.Remove (skill);
        }
        public void SetUp (IList<PassiveSkillsBase> skills) {
            PassiveSkills = skills;

            _startPassiveSkills = PassiveSkills.Where (
                (s) => s.ActivationPhase == Phase.Start
            );

            _turnPassiveSkills = PassiveSkills.Where (
                (s) => s.ActivationPhase == Phase.Turn
            );

            _endPassiveSkills = PassiveSkills.Where (
                (s) => s.ActivationPhase == Phase.End
            );
        }

        public void HandleStartSkills (bool activate) => HandleSkill (_startPassiveSkills, activate);
        public void HandleTurnSkills (bool activate) => HandleSkill (_turnPassiveSkills, activate);
        public void HandleEndSkills (bool activate) => HandleSkill (_endPassiveSkills, activate);

        private void HandleSkill (IEnumerable<PassiveSkillsBase> skills, bool activate) {
            foreach (var skill in skills) {
                if (activate) {
                    skill.Activate (Character);
                    return;
                }
                skill.Terminate (Character);
            }
        }

    }
}