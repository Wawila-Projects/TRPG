using System.Collections.Generic;
using System.Linq;

namespace Assets.Take_II.Scripts.Combat
{
    public class StatManager
    {
        public int Hp { get; }
        public int Str { get; }
        public int Mag { get; }
        public int Skl { get; }
        public int Spd { get; }
        public int Def { get; }
        public int Res { get; }
        public int Luck { get; }

        public int Movement { get; }


        public StatManager(IDictionary<string, int> stats, int movement = 2)
        {
            Hp = stats["hp"];
            Str = stats["str"];
            Mag = stats["mag"];
            Skl = stats["skl"];
            Spd = stats["spd"];
            Def = stats["def"];
            Res = stats["res"];
            Luck = stats["luck"];

            Movement = movement;
        }

        public float HitRate(params int[] modifiers)
        {
            return Skl * 2 + Luck + modifiers.DefaultIfEmpty(0).Sum();
        }

        public float Evade(params int[] modifiers)
        {
            return Spd * 2 + Luck + modifiers.DefaultIfEmpty(0).Sum();
        }

        public float Accuracy(float hitRate, float evade)
        {
            var accuracy = hitRate - evade;
            return accuracy < 0 ? 0 : accuracy * 10;
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

        public int Damage(int attackPower, int defencePower)
        {
            var damage = attackPower - defencePower;
            return damage < 0 ? 0 : damage;
        }

        public int CriticalDamge(int attackPower, int defencePower)
        {
            var damage = Damage(attackPower, defencePower);
            damage = damage == 0 ? 1 : damage * 3;
            return damage;
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
            if (!(chance < 0)) return chance > 100 ? 100 : chance;
            chance = 0;
            return chance * 10;
        }
    }
}