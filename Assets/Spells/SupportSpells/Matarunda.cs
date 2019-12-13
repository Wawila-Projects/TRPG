using System.Collections.Generic;
using Assets.CharacterSystem.PassiveSkills;
using Assets.CharacterSystem.PassiveSkills.BuffEffects;
using Assets.Enums;

namespace Assets.Spells.SupportSpells {
    public class Matarunda : SupportSpell {
        public override string Name => "Matarunda";
        public override string Description => "Decrease all foes' Attack power for 3 turns.";
        public override int Cost => 24;
        public override bool IsMultitarget => true;
        public override Elements Element => Elements.Ailment;
        public override IList<PassiveSkillsBase> Effects => new List<PassiveSkillsBase> {
            AttackEffect.GetAttackEffect(false)
        };
    }
}