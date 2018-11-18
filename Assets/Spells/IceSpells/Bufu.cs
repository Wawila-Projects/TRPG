using Assets.Take_II.Scripts.Enums;

namespace Assets.Spells.IceSpells
{
    public class Bufu : OffensiveSpell
    {
        public override int AttackPower => 80;
        public override int Accuracy => 98;
        public override Elements Element => Elements.Ice;
        protected override string Id => "Ice0";
        public override string Name => "Bufu";
        public override string Description => "Deals light Ice damage to 1 foe.	";
        public override int Cost => 4;
        public override bool IsMagical => true;
    }
}