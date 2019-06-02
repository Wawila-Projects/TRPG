using UnityEngine;
using Assets.ChracterSystem;

namespace Assets.GameSystem
{
    public sealed class TurnManager: MonoBehaviour {

        public static TurnManager Manager { get; private set; }

        public uint TurnCounter;
        public uint PlayerTurnCounter;
        public uint EnemyTurnCounter;

        public bool PlayerPhase => TurnCounter % 2 != 0;
        public bool EnemyPhase => TurnCounter % 2 == 0;
        public bool Preround => TurnCounter == 0;

        void Start()
        {
            Manager = this;
            TurnCounter = 0;
            NextTurn();
        }

        public void ResetTurnCounter()
        {
            TurnCounter = 1;
        }

        public void StartRound()
        {
            TurnCounter = 1;
        }

        public void NextTurn()
        {
            ++TurnCounter;

            if (PlayerPhase)
            {
                ++PlayerTurnCounter;
                foreach (var player in GameController.Manager.Players)
                {
                    player.TurnFinished = false;
                    player.CurrentMovement = player.Movement;
                }
                return;
            }
            
            ++EnemyTurnCounter;
            foreach (var enemy in GameController.Manager.Enemies)
            {
                enemy.TurnFinished = false;
                enemy.CurrentMovement = enemy.Movement;
            }

            Debug.Log($"Turn: {TurnCounter}");
            return;
        }
    }
}
