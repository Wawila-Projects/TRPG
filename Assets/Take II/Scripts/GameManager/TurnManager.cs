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
            }
            else if (TurnCounter > 0)
            {
                ++EnemyTurnCounter;
                EnemyPhase = true;
                PlayerPhase = false;
            }

            return TurnCounter;
        }
    }
}
