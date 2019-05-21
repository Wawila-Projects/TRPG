
using System.Collections.Generic;
using Assets.Enums;

namespace Assets.Spells.LightSpells
{
    public class Samsara : SpellBase, IChanceSpell, IExclusiveSpell
    {
        protected override string Id => "Light4";
        public override string Name => "Samsara";
        public override string Description => "Light: very high chance of instant kill, all foe.";
        public override int Cost => 44;
        public override bool IsMultitarget => true;
        public override bool IsMagical => true;
        public override Elements Element => Elements.Bless;
        public float Chance => 0.6f;
        public bool IsInstaKillSpell => true;
        public List<string> ExclusiveUnits => new List<string> {
            "Daisoujou"
        };
    }
}