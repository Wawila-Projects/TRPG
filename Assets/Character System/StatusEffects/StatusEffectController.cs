using Assets.CharacterSystem;
using Assets.Enums;
using Assets.UI;
using UnityEngine;

namespace Asstes.CharacterSystem.StatusEffects {

        [System.Serializable]
        public class StatusEffectController : MonoBehaviour {
            public StatusCondition CurrentEffect = StatusCondition.None;
            public Character Character;
            public static implicit operator StatusCondition (StatusEffectController statusEffect) {
                return statusEffect.CurrentEffect;
            }

            void Awake () {
                Character = GetComponent<Character> ();
            }

            public void SetStatusEffect (StatusCondition statusEffect) {
                if (CurrentEffect != StatusCondition.None) {
                    return;
                }

                CurrentEffect = statusEffect;
                var text = $"{statusEffect.ToString()}";

                CreateFloatingText();

                void CreateFloatingText () {
                    var element = Elements.Ailment;
                    switch (statusEffect) {
                        case StatusCondition.Shock:
                            element = Elements.Elec;
                            break;
                        case StatusCondition.Burn:
                            element = Elements.Fire;
                            break;
                        case StatusCondition.Freeze:
                            element = Elements.Ice;
                            break;
                    }

                    UIFloatingText.Create (text, Character.gameObject, element);
                }
            }

            public bool RemoveStatusEffect (StatusCondition statusEffect) {
                if (statusEffect != CurrentEffect) return false;
                CurrentEffect = StatusCondition.None;
                return true;
            }

            public void ClearStatusEffect () {
                CurrentEffect = StatusCondition.None;
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