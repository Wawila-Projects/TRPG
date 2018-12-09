using Assets.Take_II.Scripts.Enums;

namespace Assets.Spells.WindSpells
{
    public class Garu : SpellBase, IOffensiveSpell
    {
        public int AttackPower => 80;
        public int Accuracy => 98;
        public Elements Element => Elements.Wind;
        protected override string Id => "Wind0";
        public override string Name => "Garu";
        public override string Description => "Deals light Wind damage to 1 foe.";
        public override int Cost => 4;
        public override bool IsMagical => true;
        public override bool IsMultitarget => false;
    }
}