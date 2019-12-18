using System.Collections.Generic;
using Assets.CharacterSystem.PassiveSkills;
using Assets.CharacterSystem.PassiveSkills.BuffEffects;
using Assets.Enums;

namespace Assets.Spells.SupportSpells {
    public class Masakunda : SupportSpell {
        public override string Name => "Masakunda";
        public override string Description => "Decrease all foes' Agility for 3 turns.";
        public override int Cost => 24;
        public override bool IsMultitarget => true;
        public override IList<BuffEffect> Effects => new List<BuffEffect> {
            BuffEffect.AgilityEffect(false)
        };
    }
}