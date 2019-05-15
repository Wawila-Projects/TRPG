using System.Collections.Generic;
using Assets.Take_II.Scripts.Enums;

namespace Assets.Spells.ElecSpells
{
    public class ThunderReign : OffensiveSpell, IExclusiveSpell
    {
        public override int AttackPower => 400;
        public override float Accuracy => 0.98f;
        public override Elements Element => Elements.Elec;
        protected override string Id => "Elec6";
        public override string Name => "Thunder Reign";
        public override string Description => "Deals severe Elec damage to 1 foe.";
        public override int Cost => 48;
        public override bool IsMagical => true;
        public override bool IsMultitarget => false;

        public List<string> ExclusiveUnits => new List<string> {
            "Thor"
        };
    }
}