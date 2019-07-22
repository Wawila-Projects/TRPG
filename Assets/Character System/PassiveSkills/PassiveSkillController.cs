using System.Collections.Generic;
using UnityEngine;

namespace Assets.CharacterSystem.PassiveSkills {
    public class PassiveSkillController: MonoBehaviour {
        public Character Character;
        public List<PassiveSkillsBase> ActiveSkill = new List<PassiveSkillsBase>();
    }
}