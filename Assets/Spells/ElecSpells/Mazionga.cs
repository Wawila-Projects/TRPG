using Assets.Take_II.Scripts.Enums;

namespace Assets.Spells.ElecSpells
{
    public class Mazionga : SpellBase, IOffensiveSpell
    {
        public int AttackPower => 200;
        public int Accuracy => 95;
        public Elements Element => Elements.Elec;
        protected override string Id => "Elec3";
        public override string Name => "Mazionga";
        public override string Description => "Deals medium Elec damage to all foe.";
        public override int Cost => 16;
        public override bool IsMagical => true;
        public override bool IsMultitarget => true;
    }
}