using System.Collections.Generic;
using Assets.Take_II.Scripts.Enums;

namespace Assets.Take_II.Scripts.PlayerManager
{
    [System.Serializable]
    public class Stats {
        public int Strength;
        public int Magic;
        public int Endurance;
        public int Agility;
        public int Luck;

        public StatsModifiers AttackBuff;
        public StatsModifiers DefenceBuff;
        public StatsModifiers EvadeBuff;
        public StatsModifiers HitBuff;
        public bool MindCharged;
        public bool PowerCharged;

        // TODO: Add buffs for Elemental Attacks; Amp, Boost and accesories
    }
}