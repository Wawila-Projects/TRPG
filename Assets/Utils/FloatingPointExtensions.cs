using UnityEngine;

namespace Assets.Utils {
    public static partial class Extensions {
        public static bool Similar(this float lhs, float rhs, float margin = float.Epsilon)  {
            return Mathf.Abs(lhs - rhs) < margin;
        }
        public static bool Similar(this double lhs, double rhs, float margin = float.Epsilon)  {
            return ((float) lhs).Similar((float)rhs, margin);
        }
    }
}