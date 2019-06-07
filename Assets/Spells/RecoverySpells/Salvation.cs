using System;
using System.Linq;
using System.Collections.Generic;
using Asstes.CharacterSystem;
using Assets.Utils;

namespace Assets.Spells.RecoverySpells
{
    public class Salvation : RecoverySpell, IHealingSpell, IAssitSpell
    {
        protected override string Id => "Healing6";
        public override string Name => "Salvation";
        public override string Description => "Fully restores party's HP. Cures ailments.";
        public override int Cost => 40;
        public override bool IsMultitarget => true;
        public int HealingPower => 999;
        public bool FullHeal => true;
        public List<StatusConditions> CureableStatusConditions => EnumUtils<StatusConditions>.ToList();
    }
}