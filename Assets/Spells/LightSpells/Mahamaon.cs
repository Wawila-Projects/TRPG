
using Assets.Enums;

namespace Assets.Spells.LightSpells
{
    public class Mahamaon : CastableSpell, IInstantKillSpell
    {
        protected override string Id => "Light3";
        public override string Name => "Mahamaon";
        public override string Description => "Light: high chance of instant kill, all foe.";
        public override int Cost => 34;
        public override bool IsMultitarget => true;
        public override bool IsMagical => true;
        public override Elements Element => Elements.Bless;
        public float Chance => 0.4f;
    }
}