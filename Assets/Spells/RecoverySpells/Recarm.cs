namespace Assets.Spells.RecoverySpells
{
    public class Recarm : RecoverySpell, IReviveSpell
    {
        protected override string Id => "Revive0";
        public override string Name => "Recarm";
        public override string Description => "Revives 1 ally with 50% HP.";
        public override int Cost => 8;
        public override bool IsMultitarget => false;
        public float PercentageLifeRecovered => 0.5f;
    }
}