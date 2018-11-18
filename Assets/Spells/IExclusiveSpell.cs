using System.Collections.Generic;

namespace Assets.Spells
{
    public interface IExclusiveSpell
    {
        List<string> ExclusiveUnits { get; }
    }
}