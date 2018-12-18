using Assets.Take_II.Scripts.Enums;

namespace Assets.Spells
{
    public interface IChanceSpell
    {
        float Chance { get; }
        bool IsInstaKillSpell { get; }
    }
}