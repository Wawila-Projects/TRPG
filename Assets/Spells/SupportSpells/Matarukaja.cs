using System.Collections.Generic;
using Assets.CharacterSystem.PassiveSkills;
using Assets.CharacterSystem.PassiveSkills.BuffEffects;
using Assets.Enums;

namespace Assets.Spells.SupportSpells {
    public class Matarukaja : SupportSpell {
        public override string Name => "Matarukaja";
        public override string Description => "Increase party's Attack power for 3 turns.";
        public override int Cost => 24;
        public override bool IsMultitarget => true;
        public override IList<BuffEffect> Effects => new List<BuffEffect> {
            new AttackEffect(true)
        };
    }
}