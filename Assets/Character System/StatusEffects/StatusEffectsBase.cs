using System;
using Assets.ChracterSystem;

namespace Asstes.ChracterSystem.StatusEffects {
    class StatusEffectBase {
        int MaxTurnAmount;
        Character Character;
        Action ActiveEffect;

        Delegate CombatOffensiveEffect;
        Delegate CombatDiffensiveEffect;

        StatusEffectBase () {
            // CombatOffensiveEffect = new Func<int, int> (i => i);
            // CombatOffensiveEffect = new Action<int> (i => i++);
            // var a = CombatOffensiveEffect.DynamicInvoke(1);
        }

    }
}