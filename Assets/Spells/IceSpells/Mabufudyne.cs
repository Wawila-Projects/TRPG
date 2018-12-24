using Assets.Take_II.Scripts.Enums;

namespace Assets.Spells.IceSpells
{
    public class Mabufudyne : OffensiveSpell
    {
        public override int AttackPower => 320;
        public override int Accuracy => 95;
        public override Elements Element => Elements.Ice;
        public override string Name => "Mabufudyne";
        public override string Description => "Deals heavy Ice damage to all foe.";
        public override int Cost => 22;
        public override bool IsMultitarget => true;
        public override bool IsMagical => true;
        protected override string Id => "Ice5";
    }
}
