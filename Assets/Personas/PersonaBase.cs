using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Enums;
using Assets.Spells;
using Assets.Utils;
using Asstes.CharacterSystem.StatusEffects;

namespace Assets.Personas {

    [Serializable]
    public abstract class PersonaBase : UnityEngine.MonoBehaviour {
        public abstract string Name { get; }
        public int Level { get; protected set; }
        public abstract Arcana Arcana { get; }
        public abstract Elements InheritanceElement { get; }
        public SpellBook SpellBook { get; protected set; }
        public IDictionary<Elements, ResistanceModifiers> Resistances { get; protected set; }
        public virtual bool IsPlayerPersona => false;
        protected IDictionary<Statistics, int> Stats;
        protected IDictionary < Statistics, (StatsModifiers modifier, int amount) > StatBuffs;
        public int Strength => Stats[Statistics.Strength] + StatBuffs[Statistics.Strength].amount;
        public int Magic => Stats[Statistics.Magic] + StatBuffs[Statistics.Magic].amount;
        public int Endurance => Stats[Statistics.Endurance] + StatBuffs[Statistics.Endurance].amount;
        public int Agility => Stats[Statistics.Agility] + StatBuffs[Statistics.Agility].amount;
        public int Luck => Stats[Statistics.Luck] + StatBuffs[Statistics.Luck].amount;

        // TODO: Move these to passive skills controller. 
        public float CounterChance = 0f;
        public bool MindCharged = false;
        public bool PowerCharged = false;
        public bool ArmsMaster = false;
        public bool SpellMaster = false;
        public bool AptPupil = false;
        public bool DivineGrace = false;
        public IDictionary<Elements, float> ElementDamageModifier;

        // To Cast not defend 
        public IDictionary<StatusCondition, float> StatusConditionModifier;
        private IDictionary<Elements, ResistanceModifiers> OriginalResistances;

        // TODO: Add ability to buff/debuff resistances

        // protected Probability<Statistics> Probability;

        //** For Serializing */
        public double BST = 0;
        public List<string> StatsKeys;
        public List<int> StatsValues;
        public SpellBook _spellBook;
        //** End */

        public override string ToString () => $"{Level}| {Name} | {Arcana.ToString()}";

        protected virtual void Awake () {
            SpellBook = new SpellBook (this, InheritanceElement, GetBaseSpellbook (), GetLockedSpells ());

            var elements = EnumUtils<Elements>.GetValues ()
                .Where (w => w != Elements.None && w != Elements.Recovery);

            var resistances = elements.Select (s => new KeyValuePair<Elements, ResistanceModifiers> (s, ResistanceModifiers.None));
            Resistances = resistances.ToDictionary ();

            var modifiers = elements.Select (s => new KeyValuePair<Elements, float> (s, 1f));
            ElementDamageModifier = modifiers.ToDictionary ();

            StatusConditionModifier = EnumUtils<StatusCondition>.GetValues ()
                .Where (w => w != StatusCondition.None && w != StatusCondition.Down)
                .Select (s => new KeyValuePair<StatusCondition, float> (s, 1f))
                .ToDictionary ();

            SetResistances ();
            OriginalResistances = Resistances;
            Stats = GetBaseStats ();

            StatsKeys = Stats.Keys.Select ((k) => k.ToString ()).ToList ();
            StatsValues = Stats.Values.ToList ();
            _spellBook = SpellBook;
        }

        public (int newLevel, List<Statistics> statUps, ISpell spell) LevelUp (int statsUps = 3) {
            ++Level;
            var random = new Random (DateTime.Now.Millisecond);
            var statistics = EnumUtils<Statistics>.GetValues ();
            var statsToLevel = new List<Statistics> ();

            for (var i = 0; i < statsUps; ++i) {
                var stat = statistics[random.Next (statistics.Length)];
                if (Stats[stat] == 99) {
                    --i;
                    continue;
                }
                statsToLevel.Add (stat);
                ++Stats[stat];
            }

            var (_, newSpell) = SpellBook.LevelUp ();

            StatsValues = Stats.Values.ToList ();
            BST = StatsValues.Average ();

            return (Level, statsToLevel, newSpell);
        }

        public int LevelUp (List<Statistics> stats) {
            ++Level;
            foreach (var stat in stats) {
                if (Stats[stat] == 99)
                    continue;
                ++Stats[stat];
            }

            StatsValues = Stats.Values.ToList ();
            BST = StatsValues.Average ();

            return Level;
        }

        public bool BuffStats (Statistics stat, StatsModifiers modifier) {
            if (modifier == StatsModifiers.None) {
                StatBuffs[stat] = (modifier, 0);
                return true;
            }

            var currentModifier = StatBuffs[stat].modifier;
            if (currentModifier == modifier) {
                return true;
            }

            var currentStat = Stats[stat];
            if (currentModifier != StatsModifiers.None) {
                StatBuffs[stat] = (StatsModifiers.None, 0);
                return false;
            }

            if (modifier == StatsModifiers.Buff) {
                StatBuffs[stat] = (modifier, (int) Math.Round (currentStat * 1.3));
            } else {
                StatBuffs[stat] = (modifier, (int) Math.Round (currentStat * 0.7));
            }

            return true;
        }

        public bool ChangeResistances (Elements element, ResistanceModifiers modifier,
            bool clear = false, bool buff = false, bool debuff = false) {

            if (clear) {
                Resistances[element] = OriginalResistances[element];
                return true;
            }

            if (buff && modifier > Resistances[element]) {
                Resistances[element] = modifier;
                return true;
            }

            if (debuff && modifier < Resistances[element]) {
                Resistances[element] = modifier;
                return true;
            }

            if (!clear && !buff && !debuff) {
                Resistances[element] = modifier;
                return true;
            }

            return false;
        }

        protected abstract IDictionary<Statistics, int> GetBaseStats ();
        protected abstract void SetResistances ();
        protected abstract List<ISpell> GetBaseSpellbook ();
        protected abstract Dictionary<int, ISpell> GetLockedSpells ();
    }
}