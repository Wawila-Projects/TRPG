using Assets.Take_II.Scripts.Enums;

namespace Assets.Spells.LightSpells
{
    public class Mahama : SpellBase, IChanceSpell
    {
        protected override string Id => "Light1";
        public override string Name => "Mahama";
        public override string Description => "Light: low chance of instant kill, all foe.";
        public override int Cost => 18;
        public override bool IsMultitarget => true;
        public override bool IsMagical => true;
        public override Elements Element => Elements.Bless;
        public float Chance => 0.3f;
        public bool IsInstaKillSpell => true;
    }
}