using Assets.Take_II.Scripts.Enums;

namespace Assets.Spells.DarknessSpells
{
    public class Mudo : SpellBase, IElementalSpell, IChanceSpell
    {
        protected override string Id => "Darkness0";
        public override string Name => "Mudo";
        public override string Description => "Darkness: low chance of instant kill, 1 foe.";
        public override int Cost => 8;
        public override bool IsMultitarget => false;
        public override bool IsMagical => true;
        public Elements Element => Elements.Darkness;
        public float Chance => 0.4f;
        public bool IsInstaKillSpell => true;
        
    }
}