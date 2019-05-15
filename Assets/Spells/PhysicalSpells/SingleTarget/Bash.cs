namespace Assets.Spells.PhysicalSpells
{
    public class Bash : PhysicalSpell
    {
        public override string Name => "Bash";
        public override string Description => "Deals light Phys damage to 1 foe.";
        public override int Cost => 6;
        public override bool IsMultitarget => false;
        protected override string Id => "physical0";
        public override int HitCount => 1;
        public override float CriticalChance => 0.2f;
        public override int AttackPower => 120;
        public override float Accuracy => 1;
    }
}