using Assets.Enums;

namespace Assets.Spells.ElecSpells
{
    public class Ziodyne : OffensiveSpell
    {
        public override int AttackPower => 320;
        public override float Accuracy => 0.98f;
        public override Elements Element => Elements.Elec;
        protected override string Id => "Elec4";
        public override string Name => "Ziodyne";
        public override string Description => "Deals heavy Elec damage to 1 foe.";
        public override int Cost => 12;
        public override bool IsMagical => true;
        public override bool IsMultitarget => false;
    }
}