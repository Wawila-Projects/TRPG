using System.Collections.Generic;
using Assets.CharacterSystem.PassiveSkills;
using Assets.CharacterSystem.PassiveSkills.BuffEffects;
using Assets.Enums;

namespace Assets.Spells.SupportSpells {
    public class Masarukaja : SupportSpell {
        public override string Name => "Masarukaja";
        public override string Description => "Increase party's Agility for 3 turns.";
        public override int Cost => 24;
        public override bool IsMultitarget => true;
        public override IList<BuffEffect> Effects => new List<BuffEffect> {
            BuffEffect.AgilityEffect(true)
        };
    }
}