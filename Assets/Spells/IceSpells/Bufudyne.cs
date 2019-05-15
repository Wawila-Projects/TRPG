using Assets.Take_II.Scripts.Enums;

namespace Assets.Spells.IceSpells
{
    public class Bufudyne : OffensiveSpell
    {
        public override int AttackPower => 320;
        public override float Accuracy => 0.98f;
        public override Elements Element => Elements.Ice;
        public override string Name => "Bufudyne";
        public override string Description => "Deals heavy Ice damage to 1 foe.";
        public override int Cost => 12;
        public override bool IsMultitarget => false;
        public override bool IsMagical => true;
        protected override string Id => "Ice4";
    }
}
