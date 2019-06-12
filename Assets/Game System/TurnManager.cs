using Assets.CharacterSystem;
using Asstes.CharacterSystem;
using UnityEngine;

namespace Assets.GameSystem {
    public sealed class TurnManager : MonoBehaviour {

        public static TurnManager Manager { get; private set; }

        public uint TurnCounter;
        public uint PlayerTurnCounter;
        public uint EnemyTurnCounter;

        public bool PlayerPhase => TurnCounter % 2 != 0;
        public bool EnemyPhase => TurnCounter % 2 == 0;
        public bool Preround => TurnCounter == 0;

        void Start () {
            Manager = this;
            TurnCounter = 0;
            NextTurn ();
        }

        public void ResetTurnCounter () {
            TurnCounter = 1;
        }

        public void StartRound () {
            TurnCounter = 1;
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
            // TODO: Add One More triggers to Enemy
            foreach (var enemy in GameController.Manager.Enemies) {
                NewTurn (enemy);
            }
            return;

            void NewTurn (Character character) {
                character.ResetOneMore ();

                if (character.StatusEffect == StatusConditions.Dizzy) {
                    character.CurrentMovement = 0;
                    character.TurnFinished = true;
                    return;
                }

                character.TurnFinished = false;
                character.CurrentMovement = character.Movement;
            }
        }
    }
}