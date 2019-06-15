using System;
using System.Linq;
using System.Collections.Generic;

namespace Assets.Utils {
    public static partial class Extensions {

        private static Random rnd = new Random();
        public static bool IsEmpty<T>(this ICollection<T> sequence) => sequence.Count == 0;
        
        public static void Deconstruct<T1, T2>(this KeyValuePair<T1, T2> tuple, out T1 key, out T2 value)
        {
            key = tuple.Key;
            value = tuple.Value;
        }

        public static T GetRandomValue<T> (this ICollection<T> collection) {
            var index = rnd.Next(collection.Count);
            return collection.ElementAt(index);
        }

        public static void AddRange<T> (this IEnumerable<T> sequence, IEnumerable<T> other) {
            foreach (var item in other) {
                sequence.Append(item);
            }
        } 

        public static TValue GetValueOrDefault<TKey, TValue> (this IDictionary<TKey, TValue> dictionary,
            TKey key, TValue defaultValue = default (TValue)) {
            TValue value = defaultValue;
            if (dictionary?.TryGetValue (key, out value) == true) {
                return value;
            }
            return defaultValue;
        }

        public static void ForEach<T> (this IEnumerable<T> sequence, Action<T, int> action) {
            var i = 0;
            foreach (T item in sequence) {
                action (item, i);
                i++;
            }
        }

        public static void RemoveNull<T> (this IList<T> sequence) where T : class {
            for (var i = sequence.Count - 1; i >= 0; i--) {
                if (sequence[i] == null)
                    sequence.RemoveAt (i);
            }
        }
    }
}