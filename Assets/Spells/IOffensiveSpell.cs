using Assets.Take_II.Scripts.Enums;

namespace Assets.Spells
{
    public interface IOffensiveSpell: IElementalSpell
    {
        int AttackPower { get; }
        int Accuracy { get; }
    }
}