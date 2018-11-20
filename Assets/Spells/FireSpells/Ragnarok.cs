using System.Collections.Generic;
using Assets.Take_II.Scripts.Enums;

namespace Assets.Spells.FireSpells
{
    public class Ragnarok : SpellBase, IOffensiveSpell, IExclusiveSpell
    {
        public int AttackPower => 400;
        public int Accuracy => 98;
        public Elements Element => Elements.Fire;
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