
using System.Collections.Generic;
using Assets.Scripts.Enums;

namespace Assets.Spells.AlmightySpells
{
    public class Megido : OffensiveSpell
    {
        public override string Name => "Megido";

        public override string Description => "Deals medium Almighty damage to all foes.";

        public override int Cost => 18;

        public override bool IsMultitarget => true;

        public override bool IsMagical => true;

        public override Elements Element => Elements.Almighty;

        public override int AttackPower => 240;

        public override float Accuracy => 0.95f;

        protected override string Id => "Almighty0";
    }
}