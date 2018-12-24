using Assets.Take_II.Scripts.Enums;

namespace Assets.Spells.IceSpells
{
    public class Bufula : OffensiveSpell
    {
        public override int AttackPower => 200;
        public override int Accuracy => 98;
        public override Elements Element => Elements.Ice;
        public override string Name => "Bufala";
        public override string Description => "Deals medium Ice damage to 1 foe.";
        public override int Cost => 8;
        public override bool IsMultitarget => false;
        public override bool IsMagical => true;
        protected override string Id => "Ice2";
    }
}
