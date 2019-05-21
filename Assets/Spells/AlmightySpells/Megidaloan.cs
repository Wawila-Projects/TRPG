
using System.Collections.Generic;
using Assets.Scripts.Enums;

namespace Assets.Spells.AlmightySpells
{
    public class Megidolaon : OffensiveSpell
    {
        public override string Name => "Megidolain";

        public override string Description => "Deals severe Almighty damage to all foes.";

        public override int Cost => 60;

        public override bool IsMultitarget => true;

        public override bool IsMagical => true;

        public override Elements Element => Elements.Almighty;

        public override int AttackPower => 420;

        public override float Accuracy => 0.95f;

        protected override string Id => "Almighty2";
    }
}