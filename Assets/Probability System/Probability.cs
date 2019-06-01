using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Utils;

namespace Assets.ProbabilitySystem {
    public class Probability<T> {

        private IDictionary<T, int> Table;

        private int WeightAggregate;
        private Random Random;

        public Probability (IDictionary<T, int> table) {
            Table = table;
            WeightAggregate = table.Values.Sum ();
            Random = new Random ();
        }

        public IDictionary<T, float> GetProbabilityTable () {
            var table = new Dictionary<T, float> ();
            foreach (var (key, value) in Table) {
                table[key] = value / WeightAggregate;
            }
            return table;
        }

        public T GetResult (T defaultResult = default (T)) {
            var random = Random.Next (WeightAggregate);

            var cumulate = 0;
            foreach (var (key, value) in Table) {
                cumulate += value;
                if (random < cumulate) {
                    return key;
                }
            }

            return defaultResult;
        }

        public IList<T> GetResults (int amount, bool exclusive) {
            var results = new List<T> ();
            var table = new Dictionary<T, int> (Table);

            for (var i = 0; i < amount; i++) {
                if (exclusive && table.Count < amount) break;

                var result = exclusive ? exclusiveSearch() : inclusiveSearch();
                results.Add(result);
            }

            return results;

            T exclusiveSearch () {
                var result = GetResult(table, Random);
                table.Remove (result);
                return result;
            }

            T inclusiveSearch () {
                return GetResult ();
            }
        }

        public static T GetResult (IDictionary<T, int> table, Random rand = null) {
            var r = rand ?? new Random ();
            var weightAggregate = table.Values.Sum ();
            var random = r.Next (weightAggregate);

            var cumulate = 0;
            foreach (var (key, value) in table) {
                cumulate += value;
                if (random < cumulate) {
                    return key;
                }
            }

            return default (T);
        }

    }
}