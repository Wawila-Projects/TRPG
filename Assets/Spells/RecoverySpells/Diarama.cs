namespace Assets.Spells.RecoverySpells
{
    public class Diarama : RecoverySpell, IHealingSpell
    {
        protected override string Id => "Healing2";
        public override string Name => "Diarama";
        public override string Description => "Moderately restores 1 ally's HP.";
        public override int Cost => 6;
        public override bool IsMultitarget => false;
        public int HealingPower => 210;

        public bool FullHeal => false;
    }
}