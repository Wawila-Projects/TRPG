using Assets.Take_II.Scripts.Enums;

namespace Assets.Spells.ElecSpells
{
    public class Zio : SpellBase, IOffensiveSpell
    {
        public int AttackPower => 80;
        public int Accuracy => 98;
        public Elements Element => Elements.Elec;
        protected override string Id => "Elec0";
        public override string Name => "Zio";
        public override string Description => "Deals light Elec damage to 1 foe.";
        public override int Cost => 4;
        public override bool IsMagical => true;
        public override bool IsMultitarget => false;
    }
}