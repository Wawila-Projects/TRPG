namespace Assets.Spells.PhysicalSpells
{
    public class Cleave : PhysicalSpell
    {
        public override string Name => "Cleave";
        public override string Description => "Deals light Phys damage to 1 foe.";
        public override int Cost => 6;
        public override bool IsMultitarget => false;
        protected override string Id => "physical1";
        public override int HitCount => 1;
        public override float CriticalChance => 0.2f;

        public override int AttackPower => 130;

        public override float Accuracy => 1;
    }
}