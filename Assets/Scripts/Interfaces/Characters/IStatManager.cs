using System.Collections.Generic;

namespace Assets.Scripts.Interfaces.Characters
{
    public interface IStatManager
    {
        Dictionary<string, float> GetStats();
        List<string> GetStatsList();
        void CalculateStats();
    }
}
