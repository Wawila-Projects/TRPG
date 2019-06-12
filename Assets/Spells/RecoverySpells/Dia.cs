namespace Assets.Spells.RecoverySpells
{
    public class Dia : RecoverySpell, IHealingSpell
    {
        protected override string Id => "Healing0";
        public override string Name => "Dia";
        public override string Description => "Slightly restores 1 ally's HP.";
        public override int Cost => 3;
        public override bool IsMultitarget => false;
        public int HealingPower => 50;
        public bool FullHeal => false;
    }
}