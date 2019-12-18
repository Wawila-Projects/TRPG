using System.Collections.Generic;
using Assets.CharacterSystem.PassiveSkills;
using Assets.CharacterSystem.PassiveSkills.BuffEffects;
using Assets.Enums;

namespace Assets.Spells.SupportSpells {
    public class Tarunda : SupportSpell {
        public override string Name => "Tarunda";
        public override string Description => "Decrease 1 ally's Attack power for 3 turns.";
        public override int Cost => 8;
        public override bool IsMultitarget => false;
        public override IList<BuffEffect> Effects => new List<BuffEffect> {
            BuffEffect.AttackEffect(false)
        };
    }
}