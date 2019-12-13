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
        public override IList<BuffEffect> Effects => new List<BuffEffect> {
            new AttackEffect(true),
            new AgilityEffect(true),
            new DefenceEffect(true)
        };
    }
}