using System.Collections.Generic;
using Assets.Enums;
using Asstes.CharacterSystem.StatusEffects;

namespace Assets.Spells {
    public abstract class RecoverySpell : CastableSpell {
        public override Elements Element => Elements.Recovery;
        public override bool IsMagical => true;
    }

    public interface IHealingSpell {
        int HealingPower { get; }
        bool FullHeal { get; }
    }

    public interface IReviveSpell {
        float PercentageLifeRecovered { get; }
    }

    public interface IAssitSpell {
        List<StatusConditions> CureableStatusConditions { get; }
    }
}