using Assets.Take_II.Scripts.Enums;
using Assets.Take_II.Scripts.GameManager;
using Assets.Take_II.Scripts.PlayerManager;

namespace Assets.Ignore.OldElements
{
    public abstract class ElementBase
    {
        public abstract void Boon(Character target, Character caster = null);
        public abstract void Bane(Character target, Character caster = null);
        public abstract void EnvironmentalEffect();
        public Statistics PrimatyStat;
        public Statistics SecondaStat;
    }
}