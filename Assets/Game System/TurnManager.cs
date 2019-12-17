using System.Collections;
using Assets.CharacterSystem;
using Asstes.CharacterSystem.StatusEffects;
using UnityEngine;

namespace Assets.GameSystem {
    public sealed class TurnManager : MonoBehaviour {

        public static TurnManager Manager { get; private set; }

        public uint TurnCounter;
        public uint PlayerTurnCounter;
        public uint EnemyTurnCounter;

        public bool PlayerPhase => TurnCounter % 2 != 0;
        public bool EnemyPhase => TurnCounter % 2 == 0;

        void Awake () {
            Manager = this;
        }

        void Start () {
            TurnCounter = 0;
            StartRound ();

            StartCoroutine (EndRound ());
        }

        public void StartRound () {
            foreach (var player in GameController.Manager.Players) {
                player.PassiveSkills.HandleStartSkills (true);
            }

            foreach (var enemy in GameController.Manager.Enemies) {
                enemy.PassiveSkills.HandleStartSkills (true);
            }

            NextTurn ();
        }

        public IEnumerator EndRound () {
            yield return new WaitUntil (
                () => GameController.Manager.Enemies.TrueForAll (
                    e => e.IsDead
                )
            );

            Debug.Log ("Comabt Finished");
            foreach (var player in GameController.Manager.Players) {
                player.PassiveSkills.HandleEndSkills (true);
                player.PassiveSkills.ClearSkills ();
            }

            foreach (var enemy in GameController.Manager.Enemies) {
                enemy.PassiveSkills.HandleEndSkills (true);
                enemy.PassiveSkills.ClearSkills ();
            }

            yield return null;
        }

        public void NextTurn () {
            ++TurnCounter;
            Debug.Log ($"Turn: {TurnCounter}");

            if (PlayerPhase) {
                ++PlayerTurnCounter;
                foreach (var player in GameController.Manager.Players) {
                    NewTurn (player);
                }
                return;
            }

            ++EnemyTurnCounter;
            foreach (var enemy in GameController.Manager.Enemies) {
                NewTurn (enemy);
            }
            return;

            void NewTurn (Character character) {
                character.ResetOneMore ();

                if (character.StatusEffect == StatusCondition.Dizzy ||
                    character.IsDead) {
                    character.CurrentMovement = 0;
                    character.TurnFinished = true;
                    return;
                }

                character.PassiveSkills.HandleTurnSkills (true);
                character.TurnFinished = false;
                character.CurrentMovement = character.Movement;
            }
        }
    }
}