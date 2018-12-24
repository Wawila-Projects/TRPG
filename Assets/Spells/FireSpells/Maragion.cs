using Assets.Take_II.Scripts.Enums;

namespace Assets.Spells.FireSpells
{
    public class Maragion : OffensiveSpell
    {
        public override int AttackPower => 200;
        public override int Accuracy => 95;
        public override Elements Element => Elements.Fire;
        protected override string Id => "Fire3";
        public override string Name => "Maragi";
        public override string Description => "Deals medium Fire damage to all foe.";
        public override int Cost => 16;
        public override bool IsMagical => true;
        public override bool IsMultitarget => true;
    }
}