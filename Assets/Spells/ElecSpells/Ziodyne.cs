using Assets.Take_II.Scripts.Enums;

namespace Assets.Spells.ElecSpells
{
    public class Ziodyne : SpellBase, IOffensiveSpell
    {
        public int AttackPower => 320;
        public int Accuracy => 98;
        public Elements Element => Elements.Elec;
        protected override string Id => "Elec4";
        public override string Name => "Ziodyne";
        public override string Description => "Deals heavy Elec damage to 1 foe.";
        public override int Cost => 12;
        public override bool IsMagical => true;
        public override bool IsMultitarget => false;
    }
}