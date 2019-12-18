using System.Collections.Generic;
using Assets.CharacterSystem.PassiveSkills.BuffEffects;

namespace Assets.Spells.SupportSpells {
    public class Matarunda : SupportSpell {
        public override string Name => "Matarunda";
        public override string Description => "Decrease all foes' Attack power for 3 turns.";
        public override int Cost => 24;
        public override bool IsMultitarget => true;
        public override IList<BuffEffect> Effects => new List<BuffEffect> {
            BuffEffect.AttackEffect (false)
        };
    }
}