using System;
using System.Collections.Generic;

namespace Assets.Utils {
    public static partial class Extensions {

        public static bool IsEmpty<T>(this ICollection<T> sequence) => sequence.Count == 0;
        
        public static void Deconstruct<T1, T2>(this KeyValuePair<T1, T2> tuple, out T1 key, out T2 value)
        {
            key = tuple.Key;
            value = tuple.Value;
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