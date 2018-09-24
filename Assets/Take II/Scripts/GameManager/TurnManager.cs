using UnityEngine;

namespace Assets.Take_II.Scripts.GameManager
{
    public sealed class TurnManager: MonoBehaviour {

        public static TurnManager Manager { get; private set; }

        public uint TurnCounter;
        public uint PlayerTurnCounter;
        public uint EnemyTurnCounter;

        public bool PlayerPhase;
        public bool EnemyPhase;
        public bool Preround => TurnCounter == 0;

        void Awake()
        {
            Manager = this;
            TurnCounter = 0;
        }

        public void ResetTurnCounter()
        {
            TurnCounter = 1;
        }

        public void StartRound()
        {
            TurnCounter = 0;
        }

        public uint NextTurn()
        {
            ++TurnCounter;

            if (TurnCounter % 2 != 0)
            {
                ++PlayerTurnCounter;
                EnemyPhase = false;
                PlayerPhase = true;
                foreach (var player in GameController.Manager.Players)
                {
                    player.TurnFinished = false;
                }
            }
            else if (TurnCounter > 0)
            {
                ++EnemyTurnCounter;
                EnemyPhase = true;
                PlayerPhase = false;
                foreach (var enemy in GameController.Manager.Enemies)
                {
                    enemy.TurnFinished = false;
                }
            }

            return TurnCounter;
        }
    }
}
