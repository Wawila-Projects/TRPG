using UnityEngine;

namespace Assets.Take_II.Scripts.GameManager
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

        public uint NextTurn()
        {
            ++TurnCounter;

            if (TurnCounter % 2 != 0)
            {
                ++PlayerTurnCounter;
                foreach (var player in GameController.Manager.Players)
                {
                    player.TurnFinished = false;
                    player.Movement = player.Stats.Movement;
                }
            }
            else if (TurnCounter > 0)
            {
                ++EnemyTurnCounter;
                foreach (var enemy in GameController.Manager.Enemies)
                {
                    enemy.TurnFinished = false;
                    enemy.Movement = enemy.Stats.Movement;
                }
            }

            return TurnCounter;
        }
    }
}
