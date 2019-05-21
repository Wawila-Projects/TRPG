using Assets.Enums;

namespace Assets.Spells.FireSpells
{
    public class Agilao : OffensiveSpell
    {
        public override int AttackPower => 200;
        public override float Accuracy => 0.98f;
        public override Elements Element => Elements.Fire;
        protected override string Id => "Fire2";
        public override string Name => "Agilao";
        public override string Description => "Deals medium Fire damage to 1 foe.";
        public override int Cost => 8;
        public override bool IsMagical => true;
        public override bool IsMultitarget => false;
    }
}