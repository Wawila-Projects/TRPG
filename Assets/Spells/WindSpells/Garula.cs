using Assets.Take_II.Scripts.Enums;

namespace Assets.Spells.WindSpells
{
    public class Garula : SpellBase, IOffensiveSpell
    {
        public int AttackPower => 200;
        public int Accuracy => 98;
        public Elements Element => Elements.Wind;
        protected override string Id => "Wind2";
        public override string Name => "Garula";
        public override string Description => "Deals medium Wind damage to 1 foe.";
        public override int Cost => 8;
        public override bool IsMagical => true;
        public override bool IsMultitarget => false;
    }
}

/// event cell description without date