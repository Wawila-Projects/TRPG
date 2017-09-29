using System.Linq;

namespace Assets.Take_II.Scripts
{
    public class StatManager
    {
        public int Hp { private set; get; }
        public int Str { private set; get; }
        public int Mag { private set; get; }
        public int Skl { private set; get; }
        public int Spd { private set; get; }
        public int Def { private set; get; }
        public int Res { private set; get; }
        public int Luck { private set; get; }

        public StatManager(int hp, int str, int mag, int skl, int spd, int def, int res, int luck)
        {
            Hp = hp;
            Str = str;
            Mag = mag;
            Skl = skl;
            Spd = spd;
            Def = def;
            Res = res;
            Luck = luck;
        }

        public int HitRate(params int[] modifiers)
        {
            return Skl * 2 + Luck + modifiers.DefaultIfEmpty(0).Sum();
        }

        public int Avoid(params int[] modifiers)
        {
            return Spd * 2 + Luck + modifiers.DefaultIfEmpty(0).Sum();
        }

        public int Accuracy(params int[] modifiers)
        {
            return modifiers.DefaultIfEmpty(0).Sum();
        }

        public int PhysicalAttack(params int[] modifiers)
        {
            return Str + modifiers.DefaultIfEmpty(0).Sum();
        }

        public int MagicalAttack(params int[] modifiers)
        {
            return Mag + modifiers.DefaultIfEmpty(0).Sum();
        }

        public int PhysicalDefence(params int[] modifiers)
        {
            return Def + modifiers.DefaultIfEmpty(0).Sum();
        }

        public int MagicalDefence(params int[] modifiers)
        {
            return Mag + modifiers.DefaultIfEmpty(0).Sum();
        }

        public int Damge(int attackPower, int defencePower)
        {
            return attackPower - defencePower;
        }

        public int CriticalDamge(int attackPower, int defencePower)
        {
            return (attackPower - defencePower) * 3;
        }

        public float CriticalRate(params int[] modifiers)
        {
            return Skl /2 + modifiers.DefaultIfEmpty(0).Sum();
        }

        public float CriticalEvade(params int[] modifiers)
        {
            return Luck + modifiers.DefaultIfEmpty(0).Sum();
        }

        public float CriticalChance(float criticalRate, float criticalEvade)
        {
            var chance = criticalRate - criticalEvade;

            if (chance < 0)
            {
                chance = 0;
                return chance;
            }

            if (!(chance > 100)) return chance;

            chance = 100;
            return chance;
        }
    }
}