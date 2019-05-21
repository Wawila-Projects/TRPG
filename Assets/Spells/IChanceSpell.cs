using Assets.Enums;

namespace Assets.Spells
{
    public interface IChanceSpell
    {
        float Chance { get; }
        bool IsInstaKillSpell { get; }
    }
}