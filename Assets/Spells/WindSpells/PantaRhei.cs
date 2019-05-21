
using System.Collections.Generic;
using Assets.Enums;

namespace Assets.Spells.WindSpells
{
    public class PantaRhei : OffensiveSpell, IExclusiveSpell
    {
        public override int AttackPower => 400;
        public override float Accuracy => 0.98f;
        public override Elements Element => Elements.Wind;
        protected override string Id => "Wind4";
        public override string Name => "Garudyne";
        public override string Description => "Deals severe Wind damage to 1 foe.";
        public override int Cost => 48;
        public override bool IsMagical => true;
        public override bool IsMultitarget => false;

        public List<string> ExclusiveUnits => new List<string> {
            "Odin"
        };
    }
}