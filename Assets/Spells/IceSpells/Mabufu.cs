using Assets.Take_II.Scripts.Enums;

namespace Assets.Spells.IceSpells
{
    public class Mabufu : OffensiveSpell
    {
        public override int AttackPower => 80;
        public override float Accuracy => 0.95f;
        public override Elements Element => Elements.Ice;
        protected override string Id => "Ice1";
        public override string Name => "Mabufu";
        public override string Description => "Deals light Ice damage to all foes.";
        public override int Cost => 10;
        public override bool IsMagical => true;
        public override bool IsMultitarget => false;
    }
}