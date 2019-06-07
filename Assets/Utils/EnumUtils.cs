using System;
using System.Linq;
using System.Collections.Generic;

namespace Assets.Utils {
    public static class EnumUtils<T> where T : Enum {
        public static T[] GetValues() {
            return ((T[]) Enum.GetValues(typeof(T)));
        }
        public static List<T> ToList() {
            return ((T[]) Enum.GetValues(typeof(T))).ToList();
        }

    }
}
