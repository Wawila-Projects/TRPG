using Assets.Take_II.Scripts.Enums;

namespace Assets.Spells.LightSpells
{
    public class Hama : SpellBase, IChanceSpell
    {
        protected override string Id => "Light0";
        public override string Name => "Hama";
        public override string Description => "Light: low chance of instant kill, 1 foe.";
        public override int Cost => 8;
        public override bool IsMultitarget => false;
        public override bool IsMagical => true;
        public override Elements Element => Elements.Bless;
        public float Chance => 0.4f;
        public bool IsInstaKillSpell => true;
    }
}