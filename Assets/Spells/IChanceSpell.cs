using Assets.Scripts.Enums;

namespace Assets.Spells
{
    public interface IChanceSpell
    {
        float Chance { get; }
        bool IsInstaKillSpell { get; }
    }
}