using System.Collections.Generic;

namespace Assets.Utils {
    public static class Extensions {
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, 
                                                            TKey key, TValue defaultValue = default(TValue)) {
            TValue value = defaultValue;
            if(dictionary?.TryGetValue(key, out value) == true) {
                return value;
            }
            return defaultValue;
        }
    }
}