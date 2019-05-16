namespace Assets.Spells.PhysicalSpells
{
    public class SonicPunch : PhysicalSpell
    {
        public override int HitCount => 1;

        public override float CriticalChance => 0.25f;

        public override int AttackPower => 150;

        public override float Accuracy => 0.9f;

        public override string Name => "Sonic Punch";

        public override string Description => "Deals light Phys damage to 1 foe.";

        public override int Cost => 8;

        public override bool IsMultitarget => false;

        protected override string Id => "Physical3";
    }
}