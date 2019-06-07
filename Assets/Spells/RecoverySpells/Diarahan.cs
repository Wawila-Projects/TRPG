namespace Assets.Spells.RecoverySpells
{
    public class Diarahan : RecoverySpell, IHealingSpell
    {
        protected override string Id => "Healing4";
        public override string Name => "Diarahan";
        public override string Description => "Fully restores 1 ally's HP.";
        public override int Cost => 18;
        public override bool IsMultitarget => false;
        public int HealingPower => 999;
        public bool FullHeal => true;
    }
}