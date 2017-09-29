using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Interfaces.Characters;

namespace Assets.Scripts.Implementations.Stats
{
    public class MeleeStatsManager : IStatManager
    {

        private Dictionary<string, float> _meleeStats;

        public MeleeStatsManager ()
        {
            _meleeStats = new Dictionary<string, float>
            {
                {"Max Health", 100f},
                {"Current Life", 100f},
                {"Strength", 10f}
            };
        }

        public Dictionary<string, float> GetStats()
        {
            return _meleeStats;
        }

        public List<string> GetStatsList()
        {
            return _meleeStats.Keys.ToList();
        }

        public void CalculateStats()
        {
            throw new NotImplementedException();
        }
    }
}
