using Assets.Take_II.Scripts.Enums;

namespace Assets.Spells.FireSpells
{
    public class Agidyne : OffensiveSpell
    {
        public override int AttackPower => 320;
        public override int Accuracy => 98;
        public override Elements Element => Elements.Fire;
        protected override string Id => "Fire4";
        public override string Name => "Agidyne";
        public override string Description => "Deals heavy Fire damage to 1 foe.";
        public override int Cost => 12;
        public override bool IsMagical => true;
        public override bool IsMultitarget => false;
    }
}