using Assets.Scripts.Enums;

namespace Assets.Spells.WindSpells
{
    public class Garudyne : OffensiveSpell
    {
        public override int AttackPower => 320;
        public override float Accuracy => 0.98f;
        public override Elements Element => Elements.Wind;
        protected override string Id => "Wind4";
        public override string Name => "Garudyne";
        public override string Description => "Deals heavy Wind damage to 1 foe.";
        public override int Cost => 12;
        public override bool IsMagical => true;
        public override bool IsMultitarget => false;
    }
}