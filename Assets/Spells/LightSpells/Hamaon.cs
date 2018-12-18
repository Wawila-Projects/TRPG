using Assets.Take_II.Scripts.Enums;

namespace Assets.Spells.LightSpells
{
    public class Hamaon : SpellBase, IElementalSpell, IChanceSpell
    {
        protected override string Id => "Light2";
        public override string Name => "Hamaon";
        public override string Description => "Light: high chance of instant kill, 1 foe.";
        public override int Cost => 15;
        public override bool IsMultitarget => false;
        public override bool IsMagical => true;
        public Elements Element => Elements.Light;
        public float Chance => 0.6f;
        public bool IsInstaKillSpell => true; 
    }
}