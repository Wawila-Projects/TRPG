using Assets.CharacterSystem;
using Assets.CharacterSystem.PassiveSkills;
using Assets.CharacterSystem.PassiveSkills.StatusEffects;
using UnityEngine;

namespace Asstes.CharacterSystem.StatusEffects {
    [System.Serializable]
    public class StatusEffectController : MonoBehaviour {
        public StatusCondition CurrentEffect = StatusCondition.None;
        public Character Character;
        private StatusEffect _effect;

        public string Effect;
        public static implicit operator StatusCondition (StatusEffectController statusEffect) {
            return statusEffect.CurrentEffect;
        }

        void Awake () {
            Character = GetComponent<Character> ();
        }

        public void ClearStatusEffect () {
            RemoveStatusEffect (CurrentEffect);
        }

        public bool SetStatusEffect (StatusCondition statusEffect) {
            if (statusEffect == StatusCondition.Down ||
                statusEffect == StatusCondition.Dizzy) {
                ClearStatusEffect ();
            }

            if (CurrentEffect != StatusCondition.None) {
                return false;
            }
            CurrentEffect = statusEffect;
            _effect = StatusConditions.GetStatusCondition (statusEffect);
            Effect = _effect?.ToString() ?? "None";

            if (_effect is null) {
                CurrentEffect = StatusCondition.None;
                return true;
            }

            Character.PassiveSkills.AddSkill (_effect);

            if (_effect.ActivateImmediately) {
                _effect.Activate (Character);
            }

            return true;
        }

        public bool RemoveStatusEffect (StatusCondition statusEffect) {
            if (statusEffect != CurrentEffect) {
                return false;
            }
            CurrentEffect = StatusCondition.None;
            Character.PassiveSkills.RemoveSkill (_effect);
            Effect = "None";
            return true;
        }

        //     private void SetStatusEffectActions()
        //     {
        //         StatusEffectActions = new Dictionary<StatusCondition, Action> {
        //             // {
        //             //     StatusCondition.Exhaustion,
        //             //         () => Character.CurrentSP -= Mathf.RoundToInt (Character.Sp * 0.1f)
        //             // },
        //             {
        //                 StatusCondition.Burn,
        //                     () => Character.CurrentHP -= Mathf.RoundToInt (Character.Hp * 0.1f)
        //             },
        //             {
        //                 StatusCondition.Freeze,
        //                 () => {
        //                     Character.CurrentMovement = 0;
        //                     CurrentEffect = StatusCondition.None;
        //                 }
        //             },
        //             {
        //                 StatusCondition.Sleep,
        //                 () => {
        //                     Character.CurrentHP += Mathf.RoundToInt (Character.Hp * 0.05f);
        //                     Character.CurrentSP += Mathf.RoundToInt (Character.Sp * 0.05f);
        //                     Character.TurnFinished = true;
        //                 }
        //             },
        //             {
        //                 StatusCondition.Dizzy,
        //                 () => {
        //                     Character.TurnFinished = true;
        //                 }
        //             },
        //             {
        //                 StatusCondition.Dispair,
        //                 () => {
        //                     if (TurnManager.Manager.TurnCounter < FinalTurnTick)
        //                         return;
        //                     Character.CurrentHP = 0;
        //                 }
        //             }
        //         };
        //     }
        // }
    }
}