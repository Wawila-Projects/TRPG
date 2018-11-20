using System.Collections.Generic;
using Assets.Take_II.Scripts.Enums;

namespace Assets.Spells.IceSpells
{
    public class Niflheim : SpellBase, IOffensiveSpell, IExclusiveSpell
    {
        public int AttackPower => 400;
        public int Accuracy => 98;
        public Elements Element => Elements.Ice;
        public override string Name => "Niflheim";
        public override string Description => "Deals severe Ice damage to 1 foe.";
        public override int Cost => 48;
        public override bool IsMultitarget => false;
        public override bool IsMagical => true;

        public List<string> ExclusiveUnits => new List<string> {
            "Loki"
        };

        protected override string Id => "Ice6";
    }
}
