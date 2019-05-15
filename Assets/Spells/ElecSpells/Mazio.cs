using Assets.Take_II.Scripts.Enums;

namespace Assets.Spells.ElecSpells
{
    public class Mazio : OffensiveSpell
    {
        public override int AttackPower => 80;
        public override float Accuracy => 0.95f;
        public override Elements Element => Elements.Elec;
        protected override string Id => "Elec1";
        public override string Name => "Mazio";
        public override string Description => "Deals light Elec damage to all foe.";
        public override int Cost => 10;
        public override bool IsMagical => true;
        public override bool IsMultitarget => true;
    }
}