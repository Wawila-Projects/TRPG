
using System.Collections.Generic;
using Assets.Take_II.Scripts.Enums;

namespace Assets.Spells.AlmightySpells
{
    public class Megidola : OffensiveSpell
    {
        public override string Name => "Megidola";

        public override string Description => "Deals heavy Almighty damage to all foes.";

        public override int Cost => 32;

        public override bool IsMultitarget => true;

        public override bool IsMagical => true;

        public override Elements Element => Elements.Almighty;

        public override int AttackPower => 360;

        public override float Accuracy => 0.95f;

        protected override string Id => "Almighty1";
    }
}