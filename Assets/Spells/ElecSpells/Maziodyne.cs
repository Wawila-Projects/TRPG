using Assets.Scripts.Enums;

namespace Assets.Spells.ElecSpells
{
    public class Maziodyne : OffensiveSpell
    {
        public override int AttackPower => 320;
        public override float Accuracy => 0.95f;
        public override Elements Element => Elements.Elec;
        protected override string Id => "Elec5";
        public override string Name => "Maziodyne";
        public override string Description => "Deals heavy Elec damage to all foe.";
        public override int Cost => 22;
        public override bool IsMagical => true;
        public override bool IsMultitarget => true;
    }
}