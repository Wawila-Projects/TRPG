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

        private LinkedList<PassiveSkillsBase> _startPassiveSkills;
        private LinkedList<PassiveSkillsBase> _turnPassiveSkills;
        private LinkedList<PassiveSkillsBase> _endPassiveSkills;

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
                    _startPassiveSkills.AddLast (skill);
                    break;
                case Phase.Turn:
                    _turnPassiveSkills.AddLast (skill);
                    break;
                case Phase.End:
                    _endPassiveSkills.AddLast (skill);
                    break;
            }
        }
        public void RemoveSkill (PassiveSkillsBase skill) {
            if (!PassiveSkills.Remove (skill)) return;

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

            _startPassiveSkills = new LinkedList<PassiveSkillsBase> (PassiveSkills.Where (
                (s) => s.ActivationPhase == Phase.Start
            ));

            _turnPassiveSkills = new LinkedList<PassiveSkillsBase> (PassiveSkills.Where (
                (s) => s.ActivationPhase == Phase.Turn
            ));

            _endPassiveSkills = new LinkedList<PassiveSkillsBase> (PassiveSkills.Where (
                (s) => s.ActivationPhase == Phase.End
            ));
        }

        public void ClearSkills () {
            for (int i = PassiveSkills.Count - 1; i >= 0; i--) {
                PassiveSkills[i].Terminate (Character);
            }
        }

        public void HandleStartSkills (bool activate) => HandleSkill (_startPassiveSkills, activate);
        public void HandleTurnSkills (bool activate) => HandleSkill (_turnPassiveSkills, activate);
        public void HandleEndSkills (bool activate) => HandleSkill (_endPassiveSkills, activate);

        private void HandleSkill (LinkedList<PassiveSkillsBase> skills, bool activate) {
            if (skills is null || skills.IsEmpty ()) return;

            var node = skills.First;
            while (node != null) {
                var next = node.Next;
                if (activate) {
                    node.Value.Activate (Character);
                } else {
                    node.Value.Terminate (Character);
                }
                node = next;
            }
        }
    }
}