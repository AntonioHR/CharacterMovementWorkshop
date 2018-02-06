using UnityEngine;

namespace AntonioHR.Utils
{
    public static  class VectorExtensions
    {
        public static Vector2 WithX(this Vector2 vec, float x)
        {
            return new Vector2(x, vec.y);
        }
    }
}
