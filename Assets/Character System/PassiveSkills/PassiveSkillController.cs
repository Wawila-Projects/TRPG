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

        private IList<PassiveSkillsBase> _startPassiveSkills;
        private IList<PassiveSkillsBase> _turnPassiveSkills;
        private IList<PassiveSkillsBase> _endPassiveSkills;

        private void Awake () {
            if (Character == null) {
                Character = GetComponent<Character> ();
            }
        }

        private void Start () {
            SetUp (Character.Persona.SpellBook.Spells.ConvertTo<ISpell, PassiveSkillsBase> ());
        }

        public bool HasSkill (PassiveSkillsBase skill) {
            return PassiveSkills.Contains (skill);
        }
        public void AddSkill (PassiveSkillsBase skill) {
            PassiveSkills.Add (skill);
            switch (skill.ActivationPhase) {
                case Phase.Start:
                    _startPassiveSkills.Add (skill);
                    break;
                case Phase.Turn:
                    _turnPassiveSkills.Add (skill);
                    break;
                case Phase.End:
                    _endPassiveSkills.Add (skill);
                    break;
            }
        }
        public void RemoveSkill (PassiveSkillsBase skill) {
            PassiveSkills.Remove (skill);
            switch (skill.ActivationPhase) {
                case Phase.Start:
                    _startPassiveSkills.Remove (skill);
                    break;
                case Phase.Turn:
                    _turnPassiveSkills.Remove (skill);
                    break;
                case Phase.End:
                    _endPassiveSkills.Remove (skill);
                    break;
            }
        }
        public void SetUp (IList<PassiveSkillsBase> skills) {
            PassiveSkills = skills;

            _startPassiveSkills = PassiveSkills.Where (
                (s) => s.ActivationPhase == Phase.Start
            ).ToList ();

            _turnPassiveSkills = PassiveSkills.Where (
                (s) => s.ActivationPhase == Phase.Turn
            ).ToList ();

            _endPassiveSkills = PassiveSkills.Where (
                (s) => s.ActivationPhase == Phase.End
            ).ToList ();
        }

        public void ClearSkills() {
            for (int i = PassiveSkills.Count - 1; i >= 0; i--) {
                PassiveSkills[i].Terminate (Character);
            }
        }

        public void HandleStartSkills (bool activate) => HandleSkill (_startPassiveSkills, activate);
        public void HandleTurnSkills (bool activate) => HandleSkill (_turnPassiveSkills, activate);
        public void HandleEndSkills (bool activate) => HandleSkill (_endPassiveSkills, activate);

        private void HandleSkill (IList<PassiveSkillsBase> skills, bool activate) {
            if (activate) {
                foreach (var skill in skills) {
                    skill.Activate (Character);
                }
                return;
            }

            for (int i = skills.Count - 1; i >= 0; i--) {
                skills[i].Terminate (Character);
            }
        }
    }
}