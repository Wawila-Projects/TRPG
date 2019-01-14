
using System.Collections.Generic;
using Assets.Take_II.Scripts.Enums;

namespace Assets.Spells.DarknessSpells
{
    public class DieForMe : SpellBase, IChanceSpell, IExclusiveSpell
    {
        protected override string Id => "Darkness4";
        public override string Name => "Die For Me";
        public override string Description => "Darkness: very high chance of instant kill, all foe.";
        public override int Cost => 44;
        public override bool IsMultitarget => true;
        public override bool IsMagical => true;
        public override Elements Element => Elements.Curse;
        public float Chance => 0.6f;
        public bool IsInstaKillSpell => true;
        public List<string> ExclusiveUnits => new List<string> {
            "Alice"
        };
    }
}