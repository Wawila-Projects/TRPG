namespace Assets.Spells.RecoverySpells
{
    public class Samarecarm : RecoverySpell, IReviveSpell
    {
        protected override string Id => "Revive1";
        public override string Name => "Samarecarm";
        public override string Description => "Revives 1 ally with full HP.";
        public override int Cost => 18;
        public override bool IsMultitarget => false;
        public float PercentageLifeRecovered => 1f;
    }
}