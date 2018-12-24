using Assets.Take_II.Scripts.Enums;

namespace Assets.Spells.WindSpells
{
    public class Magarula : OffensiveSpell
    {
        public override int AttackPower => 200;
        public override int Accuracy => 95;
        public override Elements Element => Elements.Wind;
        protected override string Id => "Wind3";
        public override string Name => "Magarula";
        public override string Description => "Deals medium Wind damage to all foes.";
        public override int Cost => 16;
        public override bool IsMagical => true;
        public override bool IsMultitarget => true;
    }
}