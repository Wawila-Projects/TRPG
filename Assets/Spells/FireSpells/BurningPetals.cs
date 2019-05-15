using System.Collections.Generic;
using Assets.Take_II.Scripts.Enums;

namespace Assets.Spells.FireSpells
{
    public class BurningPetals : OffensiveSpell, IExclusiveSpell
    {
        public override int AttackPower => 400;
        public override float Accuracy => 0.95f;
        public override Elements Element => Elements.Fire;
        protected override string Id => "Fire7";
        public override string Name => "Burning Petals";
        public override string Description => "Deals severe Fire damage to all foe.";
        public override int Cost => 34;
        public override bool IsMagical => true;
        public override bool IsMultitarget => true;

        public List<string> ExclusiveUnits => new List<string> {
            "Sumeo-Okami"
        };
    }
}