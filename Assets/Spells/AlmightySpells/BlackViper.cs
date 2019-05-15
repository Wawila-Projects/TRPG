
using System.Collections.Generic;
using Assets.Take_II.Scripts.Enums;

namespace Assets.Spells.AlmightySpells
{
    public class BlackViper : OffensiveSpell, IExclusiveSpell
    {
        public override string Name => "Black Viper";

        public override string Description => "Deals massive Almighty damage to 1 foe.";

        public override int Cost => 64;

        public override bool IsMultitarget => false;

        public override bool IsMagical => true;

        public override Elements Element => Elements.Almighty;

        public override int AttackPower => 440;

        public override float Accuracy => 0.98f;

        public List<string> ExclusiveUnits => new List<string>() { 
            "Satan",
        };

        protected override string Id => "Almighty3";
    }
}