using Assets.Take_II.Scripts.Enums;

namespace Assets.Spells.WindSpells
{
    public class Magarudyne : OffensiveSpell
    {
        public override int AttackPower => 320;
        public override int Accuracy => 95;
        public override Elements Element => Elements.Wind;
        protected override string Id => "Wind5";
        public override string Name => "Magarudyne";
        public override string Description => "Deals heavy Wind damage to all foes.";
        public override int Cost => 22;
        public override bool IsMagical => true;
        public override bool IsMultitarget => true;
    }
}