using Assets.Take_II.Scripts.Enums;

namespace Assets.Spells.ElecSpells
{
    public class Zionga : OffensiveSpell
    {
        public override int AttackPower => 200;
        public override float Accuracy => 0.98f;
        public override Elements Element => Elements.Elec;
        protected override string Id => "Elec2";
        public override string Name => "Zionga";
        public override string Description => "Deals medium Elec damage to 1 foe.";
        public override int Cost => 8;
        public override bool IsMagical => true;
        public override bool IsMultitarget => false;
    }
}