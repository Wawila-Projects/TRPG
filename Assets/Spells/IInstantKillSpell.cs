using Assets.Enums;

namespace Assets.Spells
{
    public interface IInstantKillSpell
    {
        float Chance { get; }
        Elements Element { get; }
    }
}