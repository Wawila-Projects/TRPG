using Assets.Take_II.Scripts.Enums;

namespace Assets.Spells.WindSpells
{
    public class Magaru : SpellBase, IOffensiveSpell
    {
        public int AttackPower => 80;
        public int Accuracy => 95;
        public Elements Element => Elements.Wind;
        protected override string Id => "Wind1";
        public override string Name => "Mabufu";
        public override string Description => "Deals light Wind damage to all foes.";
        public override int Cost => 10;
        public override bool IsMagical => true;
        public override bool IsMultitarget => true;
    }
}