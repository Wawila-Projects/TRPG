
using Assets.Enums;

namespace Assets.Spells.DarknessSpells
{
    public class Mamudoon : CastableSpell, IChanceSpell
    {
        protected override string Id => "Darkness3";
        public override string Name => "Mamudoon";
        public override string Description => "Darkness: high chance of instant kill, all foe.";
        public override int Cost => 34;
        public override bool IsMultitarget => true;
        public override bool IsMagical => true;
        public override Elements Element => Elements.Curse;
        public float Chance => 0.4f;
        public bool IsInstaKillSpell => true;
        
    }
}