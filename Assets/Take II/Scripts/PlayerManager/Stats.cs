using System.Collections.Generic;
using Assets.Take_II.Scripts.Enums;

namespace Assets.Take_II.Scripts.PlayerManager
{
    [System.Serializable]
    public class Stats {
        public int Movement;
        public int Level;
        public int Hp;
        public int Sp;
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

        public Dictionary<Elements, Resistances> Resistances = new Dictionary<Elements, Resistances>
        {
            {Elements.Almighty, Enums.Resistances.None },
            {Elements.Bless, Enums.Resistances.None },
            {Elements.Curse, Enums.Resistances.None },
            {Elements.Physical, Enums.Resistances.None },
            {Elements.Elec, Enums.Resistances.None },
            {Elements.Fire, Enums.Resistances.None },
            {Elements.Ice, Enums.Resistances.None },
            {Elements.Wind, Enums.Resistances.None }
        };
    }
}