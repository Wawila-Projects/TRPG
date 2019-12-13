using System.Collections.Generic;
using Assets.CharacterSystem.PassiveSkills;
using Assets.CharacterSystem.PassiveSkills.BuffEffects;
using Assets.Enums;

namespace Assets.Spells.SupportSpells {
    public class HeatRiser : SupportSpell {
        public override string Name => "Heat Riser";
        public override string Description => "Increase 1 ally's Attack, Defense and Agility for 3 turns.";
        public override int Cost => 30;
        public override bool IsMultitarget => false;
        public override Elements Element => Elements.Recovery;
        public override IList<PassiveSkillsBase> Effects => new List<PassiveSkillsBase> {
            AttackEffect.GetAttackEffect(true),
            AgilityEffect.GetAgilityEffect(true),
            DefenceEffect.GetDefenceEffect(true)
        };
        protected override string Id => "Negatable3";
    }
}