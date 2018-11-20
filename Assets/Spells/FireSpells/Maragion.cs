using Assets.Take_II.Scripts.Enums;

namespace Assets.Spells.FireSpells
{
    public class Maragion : SpellBase, IOffensiveSpell
    {
        public int AttackPower => 200;
        public int Accuracy => 95;
        public Elements Element => Elements.Fire;
        protected override string Id => "Fire3";
        public override string Name => "Maragi";
        public override string Description => "Deals medium Fire damage to all foe.";
        public override int Cost => 16;
        public override bool IsMagical => true;
        public override bool IsMultitarget => true;
    }
}