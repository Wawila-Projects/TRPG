using Assets.Take_II.Scripts.Enums;

namespace Assets.Spells.FireSpells
{
    public class Maragi : SpellBase, IOffensiveSpell
    {
        public int AttackPower => 80;
        public int Accuracy => 95;
        public Elements Element => Elements.Fire;
        protected override string Id => "Fire1";
        public override string Name => "Maragi";
        public override string Description => "Deals light Fire damage to all foe.";
        public override int Cost => 10;
        public override bool IsMagical => true;
        public override bool IsMultitarget => true;
    }
}