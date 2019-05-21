using Assets.Enums;

namespace Assets.Spells.FireSpells
{
    public class Maragidyne : OffensiveSpell
    {
        public override int AttackPower => 320;
        public override float Accuracy => 0.95f;
        public override Elements Element => Elements.Fire;
        protected override string Id => "Fire5";
        public override string Name => "Maragidyne";
        public override string Description => "Deals heavy Fire damage to all foe.";
        public override int Cost => 22;
        public override bool IsMagical => true;
        public override bool IsMultitarget => true;
    }
}