using Assets.Take_II.Scripts.Enums;

namespace Assets.Spells.DarknessSpells
{
    public class Mudoon : SpellBase, IElementalSpell, IChanceSpell
    {
        protected override string Id => "Darkness2";
        public override string Name => "Mudoon";
        public override string Description => "Darkness: high chance of instant kill, 1 foe.";
        public override int Cost => 15;
        public override bool IsMultitarget => false;
        public override bool IsMagical => true;
        public Elements Element => Elements.Darkness;
        public float Chance => 0.6f;
        public bool IsInstaKillSpell => true;
        
    }
}