using Assets.Take_II.Scripts.Enums;

namespace Assets.Spells.FireSpells
{
    public class Maragidyne : SpellBase, IOffensiveSpell
    {
        public int AttackPower => 320;
        public int Accuracy => 95;
        public Elements Element => Elements.Fire;
        protected override string Id => "Fire5";
        public override string Name => "Maragidyne";
        public override string Description => "Deals heavy Fire damage to all foe.";
        public override int Cost => 22;
        public override bool IsMagical => true;
        public override bool IsMultitarget => true;
    }
}