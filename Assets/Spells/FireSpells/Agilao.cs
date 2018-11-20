using Assets.Take_II.Scripts.Enums;

namespace Assets.Spells.FireSpells
{
    public class Agilao : SpellBase, IOffensiveSpell
    {
        public int AttackPower => 200;
        public int Accuracy => 98;
        public Elements Element => Elements.Fire;
        protected override string Id => "Fire2";
        public override string Name => "Agilao";
        public override string Description => "Deals medium Fire damage to 1 foe.";
        public override int Cost => 8;
        public override bool IsMagical => true;
        public override bool IsMultitarget => false;
    }
}