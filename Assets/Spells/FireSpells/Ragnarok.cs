using System.Collections.Generic;
using Assets.Enums;

namespace Assets.Spells.FireSpells
{
    public class Ragnarok : OffensiveSpell, IExclusiveSpell
    {
        public override int AttackPower => 400;
        public override float Accuracy => 0.98f;
        public override Elements Element => Elements.Fire;
        protected override string Id => "Fire6";
        public override string Name => "Ragnarok";
        public override string Description => "Deals severe Fire damage to 1 foe.";
        public override int Cost => 48;
        public override bool IsMagical => true;
        public override bool IsMultitarget => false;
        public List<string> ExclusiveUnits => new List<string> {
            "Surt"
        };
    }
}