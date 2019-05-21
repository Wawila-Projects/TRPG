using Assets.Scripts.Enums;

namespace Assets.Spells.ElecSpells
{
    public class Mazionga : OffensiveSpell
    {
        public override int AttackPower => 200;
        public override float Accuracy => 0.95f;
        public override Elements Element => Elements.Elec;
        protected override string Id => "Elec3";
        public override string Name => "Mazionga";
        public override string Description => "Deals medium Elec damage to all foe.";
        public override int Cost => 16;
        public override bool IsMagical => true;
        public override bool IsMultitarget => true;
    }
}