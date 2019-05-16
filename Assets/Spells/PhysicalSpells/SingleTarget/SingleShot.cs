namespace Assets.Spells.PhysicalSpells
{
   public class SingleShot : PhysicalSpell
    {
         public override int HitCount => 1;

        public override float CriticalChance => 0.25f;

        public override int AttackPower => 170;

        public override float Accuracy => 0.9f;

        public override string Name => "Double Fangs";

        public override string Description => "Deals light Phys damage to 1 foe.";

        public override int Cost => 8;

        public override bool IsMultitarget => false;

        protected override string Id => "Physical5";
    }
}