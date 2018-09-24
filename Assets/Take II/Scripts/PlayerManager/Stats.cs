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

        public bool AttackBuff;
        public bool DefenceBuff;
        public bool EvadeBuff;
        public bool HitBuff;

        public Dictionary<Elements, Resistances> Resistances = new Dictionary<Elements, Resistances>
        {
            {Elements.Almighty, Enums.Resistances.None },
            {Elements.Light, Enums.Resistances.None },
            {Elements.Darkness, Enums.Resistances.None },
            {Elements.Physical, Enums.Resistances.None },
            {Elements.Elec, Enums.Resistances.None },
            {Elements.Fire, Enums.Resistances.None },
            {Elements.Ice, Enums.Resistances.None },
            {Elements.Wind, Enums.Resistances.None }
        };
    }
}