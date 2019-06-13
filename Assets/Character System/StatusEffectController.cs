using System;
using System.Collections;
using System.Collections.Generic;
using Assets.CharacterSystem;
using Assets.Enums;
using Assets.GameSystem;
using Assets.PlayerSystem;
using Assets.Spells;
using UnityEngine;

namespace Asstes.CharacterSystem {
    public class StatusEffectController : MonoBehaviour {
        public StatusConditions CurrentEffect = StatusConditions.None;
        // public TurnManager TurnManager;
        public Character Character;
        private uint LastTurnTick;
        private int FinalTurnTick = -1;

        [SerializeField]
        private bool IsPlayer;

        private Coroutine ActiveCoroutine;

        private IDictionary<StatusConditions, Action> StatusEffectActions;

        public static implicit operator StatusConditions (StatusEffectController statusEffect) {
            return statusEffect.CurrentEffect;
        }

        void Awake ()
        {
            Character = GetComponent<Character>();
            IsPlayer = Character is Player;
            SetStatusEffectActions();
        }

        public void SetStatusEffect (StatusConditions statusEffect) {
            if (statusEffect <= CurrentEffect) {
                return;
            }

            // if (!StatusEffectActions.ContainsKey(statusEffect)) {
            //     return;
            // }

            if (statusEffect == StatusConditions.Dispair) 
                FinalTurnTick = (int)TurnManager.Manager.TurnCounter + 7;

            CurrentEffect = statusEffect;
            // var action = StatusEffectActions[statusEffect];
            // ActiveCoroutine = StartCoroutine (ActiveStatusEffect (action));


            var text = $"{statusEffect.ToString()}";

            var element = Elements.Ailment;
            switch(statusEffect) {
                case StatusConditions.Shock:
                    element = Elements.Elec;
                    break;
                case StatusConditions.Burn:
                    element = Elements.Fire;
                    break;
                case StatusConditions.Freeze:
                    element = Elements.Ice;
                    break;
            }

            UIFloatingText.Create(text, Character.gameObject, element);
        }

        public bool RemoveStatusEffect (StatusConditions statusEffect) {
            if (statusEffect != CurrentEffect) return false;
            if (CurrentEffect == StatusConditions.None) return false;
            if (ActiveCoroutine == null) return false;
            StopCoroutine (ActiveCoroutine);
            CurrentEffect = StatusConditions.None;
            return true;
        }

        IEnumerator ActiveStatusEffect (Action action) {
            while (true) {
                var (validTurn, currentTurn) = checkTurn ();

                if (!validTurn) {
                    yield return null;
                    continue;
                }

                action ();
                Debug.Log ($"Effect {CurrentEffect} |  Turn {currentTurn}");

                LastTurnTick = currentTurn;
                if (LastTurnTick == FinalTurnTick) {
                    break;
                }

                yield return null;
            }
            yield return null;
        }

        private (bool validTurn, uint currentTurn) checkTurn () {
            var currentTurn = TurnManager.Manager.TurnCounter;
            if (LastTurnTick == currentTurn) {
                return (false, currentTurn);
            }
            if (IsPlayer && TurnManager.Manager.PlayerPhase) {
                return (true, currentTurn);
            }
            if (!IsPlayer && TurnManager.Manager.EnemyPhase) {
                return (true, currentTurn);
            }
            return (false, currentTurn);
        }

        private void SetStatusEffectActions()
        {
            StatusEffectActions = new Dictionary<StatusConditions, Action> {
                // {
                //     StatusConditions.Exhaustion,
                //         () => Character.CurrentSP -= Mathf.RoundToInt (Character.Sp * 0.1f)
                // },
                {
                    StatusConditions.Burn,
                        () => Character.CurrentHP -= Mathf.RoundToInt (Character.Hp * 0.1f)
                },
                {
                    StatusConditions.Freeze,
                    () => {
                        Character.CurrentMovement = 0;
                        CurrentEffect = StatusConditions.None;
                    }
                },
                {
                    StatusConditions.Sleep,
                    () => {
                        Character.CurrentHP += Mathf.RoundToInt (Character.Hp * 0.05f);
                        Character.CurrentSP += Mathf.RoundToInt (Character.Sp * 0.05f);
                        Character.TurnFinished = true;
                    }
                },
                {
                    StatusConditions.Dizzy,
                    () => {
                        Character.TurnFinished = true;
                    }
                },
                {
                    StatusConditions.Dispair,
                    () => {
                        if (TurnManager.Manager.TurnCounter < FinalTurnTick)
                            return;
                        Character.CurrentHP = 0;
                    }
                }
            };
        }
    }
}