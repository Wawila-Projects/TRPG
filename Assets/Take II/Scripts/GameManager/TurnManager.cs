namespace Assets.Take_II.Scripts.Combat
{
    public sealed class TurnManager {

        public static TurnManager Manager { get; } = new TurnManager();

        public uint TurnCounter { get; private set; }
        public uint PlayerTurnCounter { get; private set; }
        public uint EnemyTurnCounter { get; private set; }

        public bool PlayerPhase { get; private set; }
        public bool EnemyPhase { get; private set; }
        public bool Preround => TurnCounter == 0;

        public TurnManager()
        {
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
