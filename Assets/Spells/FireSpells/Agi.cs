using Assets.Take_II.Scripts.Enums;

namespace Assets.Spells.FireSpells
{
    public class Agi : SpellBase, IOffensiveSpell
    {
        public int AttackPower => 80;
        public int Accuracy => 98;
        public Elements Element => Elements.Fire;
        protected override string Id => "Fire0";
        public override string Name => "Agi";
        public override string Description => "Deals light Fire damage to 1 foe.";
        public override int Cost => 4;
        public override bool IsMagical => true;
        public override bool IsMultitarget => false;
    }
}