using Assets.Scripts.Enums;

namespace Assets.Spells.WindSpells
{
    public class Magaru : OffensiveSpell
    {
        public override int AttackPower => 80;
        public override float Accuracy => 0.95f;
        public override Elements Element => Elements.Wind;
        protected override string Id => "Wind1";
        public override string Name => "Mabufu";
        public override string Description => "Deals light Wind damage to all foes.";
        public override int Cost => 10;
        public override bool IsMagical => true;
        public override bool IsMultitarget => true;
    }
}