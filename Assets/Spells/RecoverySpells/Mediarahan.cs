namespace Assets.Spells.RecoverySpells
{
    public class Mediarahan : RecoverySpell, IHealingSpell
    {
        protected override string Id => "Healing5";
        public override string Name => "Mediarahan";
        public override string Description => "Fully restores party's HP.";
        public override int Cost => 30;
        public override bool IsMultitarget => true;
        public int HealingPower => 999;
        public bool FullHeal => true;
    }
}