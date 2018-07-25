using System.Collections.Generic;
using System.Linq;
using Assets.Take_II.Scripts.Enums;

namespace Assets.Take_II.Scripts.PlayerManager
{
    public class StatManager
    {
        private readonly IDictionary<Statistics, int> _buffs;
        public IDictionary<Statistics, int> AllStats { get; }

        public int Hp => AllStats[Statistics.Hp] + _buffs[Statistics.Hp];

        public int Str => AllStats[Statistics.Str] + _buffs[Statistics.Str];

        public int Mag => AllStats[Statistics.Mag] + _buffs[Statistics.Mag];

        public int Skl => AllStats[Statistics.Skl] + _buffs[Statistics.Skl];

        public int Spd => AllStats[Statistics.Spd] + _buffs[Statistics.Spd];

        public int Def => AllStats[Statistics.Def] + _buffs[Statistics.Def];

        public int Res => AllStats[Statistics.Res] + _buffs[Statistics.Res];

        public int Luck => AllStats[Statistics.Luck] + _buffs[Statistics.Luck];

        public int Movement { get; }
        

        public StatManager(IDictionary<Statistics, int> stats, int movement = 2)
        {
            AllStats = stats;
            Movement = movement;

            _buffs = new Dictionary<Statistics, int>
            {
                {Statistics.Hp, 0},
                {Statistics.Str, 0},
                {Statistics.Mag, 0},
                {Statistics.Skl, 0},
                {Statistics.Spd, 0},
                {Statistics.Def, 0},
                {Statistics.Res, 0},
                {Statistics.Luck, 0}
            };
        }

        public void ModifyBuffs(IDictionary<Statistics, int> stats, bool remove = false)
        {
            foreach (var buff in stats)
            {
                _buffs[buff.Key] += remove ? stats[buff.Key] : -stats[buff.Key];
            }
        }

        public void ModifySingleBuff(Statistics stat, int amount)
        {
            _buffs[stat] += amount;
        }

        public void LevelUp(IDictionary<Statistics, int> statGrowth)
        {
            foreach (var stat in statGrowth)
            {
                AllStats[stat.Key] += stat.Value;
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
            return Res + modifiers.DefaultIfEmpty(0).Sum();
        }

        public int Damage(int attackPower, int defencePower)
        {
            var damage = attackPower - defencePower;
            return damage < 0 ? 0 : damage;
        }

        public int CriticalDamage(int attackPower, int defencePower)
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
            if (chance < 0) return 0;
            chance = chance > 100 ? 100 : chance;
            return chance;
        }

        public int Heal(params int[] modifiers)
        {
            var heal = Mag/3 + modifiers.DefaultIfEmpty(0).Sum();
            return heal < 1 ? 1 : heal;
        }
    }
}