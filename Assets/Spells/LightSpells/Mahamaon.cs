
using Assets.Take_II.Scripts.Enums;

namespace Assets.Spells.LightSpells
{
    public class Mahamaon : SpellBase, IElementalSpell, IChanceSpell
    {
        protected override string Id => "Light3";
        public override string Name => "Mahamaon";
        public override string Description => "Light: high chance of instant kill, all foe.";
        public override int Cost => 34;
        public override bool IsMultitarget => true;
        public override bool IsMagical => true;
        public Elements Element => Elements.Light;
        public float Chance => 0.4f;
        public bool IsInstaKillSpell => true;
    }
}