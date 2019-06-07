namespace Assets.Spells.RecoverySpells
{
    public class Media : RecoverySpell, IHealingSpell
    {
        protected override string Id => "Healing1";
        public override string Name => "Media";
        public override string Description => "Slightly restores party's HP.";
        public override int Cost => 7;
        public override bool IsMultitarget => true;
        public int HealingPower => 75;
        public bool FullHeal => false;
    }
}