using System.Collections;
using System.Linq;
using Assets.CharacterSystem;
using Assets.Utils;
using UnityEngine;

namespace Assets.GameSystem {

    [RequireComponent (typeof (GameController))]
    public sealed class TurnManager : MonoBehaviour {

        public static TurnManager Manager { get; private set; }

        public GameController GameController;

        public uint TurnCounter;

        public bool PlayerPhase => TurnCounter % 2 != 0;
        public bool EnemyPhase => TurnCounter % 2 == 0;

        void Awake () {
            Manager = this;
            if (GameController is null) {
                GameController = GetComponent<GameController> ();
            }
        }

        void Start () {
            TurnCounter = 0;
            StartRound ();

            StartCoroutine (EndRound ());
        }

        public void StartRound () {
            foreach (var player in GameController.Players) {
                player.PassiveSkills.HandleStartSkills (true);
            }

            foreach (var enemy in GameController.Enemies) {
                enemy.PassiveSkills.HandleStartSkills (true);
            }

            NextTurn ();
        }

        public IEnumerator EndRound () {
            yield return new WaitUntil (
                () =>
                !GameController.Enemies.IsEmpty () &&
                GameController.Enemies.TrueForAll (
                    e => e.IsDead
                )
            );

            Debug.Log ("Comabt Finished");
            foreach (var player in GameController.Players) {
                player.PassiveSkills.HandleEndSkills (true);
                player.PassiveSkills.ClearSkills ();
            }

            foreach (var enemy in GameController.Enemies) {
                enemy.PassiveSkills.HandleEndSkills (true);
                enemy.PassiveSkills.ClearSkills ();
            }

            yield return null;
        }

        public void NextTurn () {
            var characters = PlayerPhase ?
                GameController.Players.Cast<Character> () :
                GameController.Enemies.Cast<Character> ();

            foreach (var character in characters) {
                NewTurn (character);
            }

            ++TurnCounter;
            Debug.Log ($"Turn: {TurnCounter} {PlayerPhase}");

            void NewTurn (Character character) {
                character.ResetOneMore ();
                character.TurnFinished = false;
                character.CurrentMovement = character.Movement;
                character.PassiveSkills.HandleTurnSkills (true);
            }
        }
    }
}