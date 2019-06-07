using System.Collections.Generic;
using Asstes.CharacterSystem;

namespace Assets.Spells.RecoverySpells
{
    public class RePatra : RecoverySpell, IAssitSpell
    {
        protected override string Id => "Assist2";
        public override string Name => "Re Patra";
        public override string Description => "Recovers 1 ally from Down or Dizzy status.";
        public override int Cost => 3;
        public override bool IsMultitarget => false;
        public List<StatusConditions> CureableStatusConditions => new List<StatusConditions> {
            StatusConditions.Dizzy, StatusConditions.Down, 
        };
    }
}