using Assets.Take_II.Scripts.Enums;

namespace Assets.Spells.WindSpells
{
    public class Garula : OffensiveSpell
    {
        public override int AttackPower => 200;
        public override int Accuracy => 98;
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