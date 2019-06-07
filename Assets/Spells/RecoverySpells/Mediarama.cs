namespace Assets.Spells.RecoverySpells
{
    public class Mediarama : RecoverySpell, IHealingSpell
    {
        protected override string Id => "Healing3";
        public override string Name => "Mediarama";
        public override string Description => "Moderately restores 1 ally's HP.";
        public override int Cost => 12;
        public override bool IsMultitarget => true;
        public int HealingPower => 210;
        public bool FullHeal => false;
    }
}