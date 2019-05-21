using Assets.Enums;

namespace Assets.Spells.WindSpells
{
    public class Garula : OffensiveSpell
    {
        public override int AttackPower => 200;
        public override float Accuracy => 0.98f;
        public override Elements Element => Elements.Wind;
        protected override string Id => "Wind2";
        public override string Name => "Garula";
        public override string Description => "Deals medium Wind damage to 1 foe.";
        public override int Cost => 8;
        public override bool IsMagical => true;
        public override bool IsMultitarget => false;
    }
}

/// event cell description without date