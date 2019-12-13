using System.Collections.Generic;
using Assets.CharacterSystem.PassiveSkills;
using Assets.CharacterSystem.PassiveSkills.BuffEffects;
using Assets.Enums;

namespace Assets.Spells.SupportSpells {
    public class Rakukaja : SupportSpell {
        public override string Name => "Rakukaja";
        public override string Description => "Increase 1 ally's Defence for 3 turns.";
        public override int Cost => 8;
        public override bool IsMultitarget => false;
        public override Elements Element => Elements.Recovery;
        public override IList<PassiveSkillsBase> Effects => new List<PassiveSkillsBase> {
            DefenceEffect.GetDefenceEffect(true)
        };
    }
}