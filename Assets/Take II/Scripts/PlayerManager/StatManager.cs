using System.Collections.Generic;
using System.Linq;
using Assets.Take_II.Scripts.Enums;

namespace Assets.Take_II.Scripts.PlayerManager
{
    public class StatManager
    {
        private readonly IDictionary<Statistics, int> _stats;

        public int Hp { get { return _stats[Statistics.Hp]; } }

        public int Str { get { return _stats[Statistics.Str]; } }

        public int Mag { get { return _stats[Statistics.Mag]; } }

        public int Skl { get { return _stats[Statistics.Skl]; } }

        public int Spd { get { return _stats[Statistics.Spd]; } }

        public int Def { get { return _stats[Statistics.Def]; } }

        public int Res { get { return _stats[Statistics.Res]; } }

        public int Luck { get { return _stats[Statistics.Luck]; } }

        public int Movement { get; private set; }


        public StatManager(IDictionary<Statistics, int> stats, int movement = 2)
        {
            _stats = stats;
            Movement = movement;
        }

        public void LevelUp(IDictionary<Statistics, int> statGrowth)
        {
            foreach (var stat in statGrowth)
            {
                _stats[stat.Key] += stat.Value;
            }
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