namespace Assets.Spells.PhysicalSpells
{
    public class DoubleFangs : PhysicalSpell
    {
        public override int HitCount => 2;

        public override float CriticalChance => 0.15f;

        public override int AttackPower => 80;

        public override float Accuracy => 0.9f;

        public override string Name => "Double Fangs";

        public override string Description => "Deals light Phys damage to 1 foe 2x.";

        public override int Cost => 8;

        public override bool IsMultitarget => false;

        protected override string Id => "Physical4";
    }
}