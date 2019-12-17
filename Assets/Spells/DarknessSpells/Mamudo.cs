
using Assets.Enums;

namespace Assets.Spells.DarknessSpells
{
    public class Mamudo : CastableSpell, IInstantKillSpell
    {
        protected override string Id => "Darkness1";
        public override string Name => "Mamudo";
        public override string Description => "Darkness: low chance of instant kill, all foe.";
        public override int Cost => 18;
        public override bool IsMultitarget => true;
        public override bool IsMagical => true;
        public override Elements Element => Elements.Curse;
        public float Chance => 0.3f;
        
    }
}