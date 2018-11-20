using Assets.Take_II.Scripts.Enums;

namespace Assets.Spells.IceSpells
{
    public class Mabufula : SpellBase, IOffensiveSpell
    {
        public int AttackPower => 200;
        public int Accuracy => 95;
        public Elements Element => Elements.Ice;
        public override string Name => "Mabufala";
        public override string Description => "Deals medium Ice damage to all foe.";
        public override int Cost => 16;
        public override bool IsMultitarget => true;
        public override bool IsMagical => true;
        protected override string Id => "Ice3";
    }
}
