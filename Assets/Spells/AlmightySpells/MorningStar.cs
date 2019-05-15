
using System.Collections.Generic;
using Assets.Take_II.Scripts.Enums;

namespace Assets.Spells.AlmightySpells
{
    public class MorningStar : OffensiveSpell, IExclusiveSpell
    {
        public override string Name => "Morning Star";

        public override string Description => "Deals massive Almighty damage to all foe.";

        public override int Cost => 72;

        public override bool IsMultitarget => true;

        public override bool IsMagical => true;

        public override Elements Element => Elements.Almighty;

        public override int AttackPower => 440;

        public override float Accuracy => 0.95f;

        public List<string> ExclusiveUnits => new List<string>() { 
            "Helel",
        };

        protected override string Id => "Almighty4";
    }
}