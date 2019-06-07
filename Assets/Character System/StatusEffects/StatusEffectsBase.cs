using System;
using Assets.CharacterSystem;

namespace Asstes.CharacterSystem.StatusEffects {
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