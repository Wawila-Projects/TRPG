using Assets.Take_II.Scripts.Enums;

namespace Assets.Spells.WindSpells
{
    public class Garudyne : SpellBase, IOffensiveSpell
    {
        public int AttackPower => 320;
        public int Accuracy => 98;
        public Elements Element => Elements.Wind;
        protected override string Id => "Wind4";
        public override string Name => "Garudyne";
        public override string Description => "Deals heavy Wind damage to 1 foe.";
        public override int Cost => 12;
        public override bool IsMagical => true;
        public override bool IsMultitarget => false;
    }
}