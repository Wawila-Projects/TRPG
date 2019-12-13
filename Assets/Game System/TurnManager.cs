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
        public bool Preround => TurnCounter == 0;

        void Awake() {
            Manager = this;
        }

        void Start () {
            StartRound ();
        }

        public void StartRound () {
            TurnCounter = 1;

            foreach (var player in GameController.Manager.Players) {
                player.PassiveSkills.HandleStartSkills (true);
            }

            foreach (var enemy in GameController.Manager.Enemies) {
                enemy.PassiveSkills.HandleStartSkills (true);
            }
        }

        public void EndRound () {
            foreach (var player in GameController.Manager.Players) {
                player.PassiveSkills.HandleEndSkills (true);
                player.PassiveSkills.HandleStartSkills (false);
                player.PassiveSkills.HandleTurnSkills (false);
            }

            foreach (var enemy in GameController.Manager.Enemies) {
                enemy.PassiveSkills.HandleEndSkills (true);
                enemy.PassiveSkills.HandleStartSkills (false);
                enemy.PassiveSkills.HandleTurnSkills (false);
            }
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

                if (character.StatusEffect == StatusConditions.Dizzy ||
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