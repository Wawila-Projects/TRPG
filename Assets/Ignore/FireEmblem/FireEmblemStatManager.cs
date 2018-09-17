// using System.Collections.Generic;
// using System.Linq;
// using Assets.Take_II.Scripts.Enums;

// namespace Assets.Take_II.Scripts.PlayerManager
// {
//     public class FireEmblemStatManager: StatsBase
//     {
//         private readonly IDictionary<Statistics, int> _buffs;
//         public IDictionary<Statistics, int> AllStats { get; }

//         public new int Hp => AllStats[Statistics.Hp] + _buffs[Statistics.Hp];

//         public new int Strength => AllStats[Statistics.Strength] + _buffs[Statistics.Strength];

//         public new int Magic => AllStats[Statistics.Magic] + _buffs[Statistics.Magic];

//         public new int Skill => AllStats[Statistics.Skill] + _buffs[Statistics.Skill];

//         public new int Speed => AllStats[Statistics.Speed] + _buffs[Statistics.Speed];

//         public new int Defence => AllStats[Statistics.Defence] + _buffs[Statistics.Defence];

//         public new int Resistance => AllStats[Statistics.Resistance] + _buffs[Statistics.Resistance];

//         public new int Luck => AllStats[Statistics.Luck] + _buffs[Statistics.Luck];

//         public new int Movement { get; }
        

//         public FireEmblemStatManager(IDictionary<Statistics, int> stats, int movement = 2): base(movement, stats)
//         {
//             AllStats = stats;
//             Movement = movement;

//             _buffs = new Dictionary<Statistics, int>
//             {
//                 {Statistics.Hp, 0},
//                 {Statistics.Strength, 0},
//                 {Statistics.Magic, 0},
//                 {Statistics.Skill, 0},
//                 {Statistics.Speed, 0},
//                 {Statistics.Defence, 0},
//                 {Statistics.Resistance, 0},
//                 {Statistics.Luck, 0}
//             };
//         }

//         public void ModifyBuffs(IDictionary<Statistics, int> stats, bool remove = false)
//         {
//             foreach (var buff in stats)
//             {
//                 _buffs[buff.Key] += remove ? stats[buff.Key] : -stats[buff.Key];
//             }
//         }

//         public void ModifySingleBuff(Statistics stat, int amount)
//         {
//             _buffs[stat] += amount;
//         }

//         public void LevelUp(IDictionary<Statistics, int> statGrowth)
//         {
//             foreach (var stat in statGrowth)
//             {
//                 AllStats[stat.Key] += stat.Value;
//             }
//         }

//         public float HitRate(params int[] modifiers)
//         {
//             return Skill * 2 + Luck + modifiers.DefaultIfEmpty(0).Sum();
//         }

//         public float Evade(params int[] modifiers)
//         {
//             return Speed * 2 + Luck + modifiers.DefaultIfEmpty(0).Sum();
//         }

//         public float Accuracy(float hitRate, float evade)
//         {
//             var accuracy = hitRate - evade;
//             return accuracy < 0 ? 0 : accuracy * 10;
//         }

//         public int PhysicalAttack(params int[] modifiers)
//         {
//             return Strength + modifiers.DefaultIfEmpty(0).Sum();
//         }

//         public int MagicalAttack(params int[] modifiers)
//         {
//             return Magic + modifiers.DefaultIfEmpty(0).Sum();
//         }

//         public int PhysicalDefence(params int[] modifiers)
//         {
//             return Defence + modifiers.DefaultIfEmpty(0).Sum();
//         }

//         public int MagicalDefence(params int[] modifiers)
//         {
//             return Resistance + modifiers.DefaultIfEmpty(0).Sum();
//         }

//         public int Damage(int attackPower, int defencePower)
//         {
//             var damage = attackPower - defencePower;
//             return damage < 0 ? 0 : damage;
//         }

//         public int CriticalDamage(int attackPower, int defencePower)
//         {
//             var damage = Damage(attackPower, defencePower);
//             damage = damage == 0 ? 1 : damage * 3;
//             return damage;
//         }

//         public float CriticalRate(params int[] modifiers)
//         {
//             return Skill /2 + modifiers.DefaultIfEmpty(0).Sum();
//         }

//         public float CriticalEvade(params int[] modifiers)
//         {
//             return Luck + modifiers.DefaultIfEmpty(0).Sum();
//         }

//         public float CriticalChance(float criticalRate, float criticalEvade)
//         {
//             var chance = criticalRate - criticalEvade;
//             if (chance < 0) return 0;
//             chance = chance > 100 ? 100 : chance;
//             return chance;
//         }

//         public int Heal(params int[] modifiers)
//         {
//             var heal =  Magic/3 + modifiers.DefaultIfEmpty(0).Sum();
//             return heal < 1 ? 1 : heal;
//         }
//     }
// }