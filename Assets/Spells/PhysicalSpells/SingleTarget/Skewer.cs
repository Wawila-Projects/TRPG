namespace Assets.Spells.PhysicalSpells
{
    public class Skewer : PhysicalSpell
    {
        public override int HitCount => 1;

        public override float CriticalChance => 0.2f;

        public override int AttackPower => 140;

        public override float Accuracy => 0.85f;

        public override string Name => "Skewer";

        public override string Description => "Deals light Phys damage to 1 foe.";

        public override int Cost => 5;

        public override bool IsMultitarget => false;

        protected override string Id => "physical2";
    }
}