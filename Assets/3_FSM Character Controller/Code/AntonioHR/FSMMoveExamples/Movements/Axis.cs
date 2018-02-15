using UnityEngine;


namespace AntonioHR.FSMMoveExamples.Movements
{

    public enum Axis2 { X, Y }


    public static class Axis2Helpers
    {
        public static Axis2 Other(this Axis2 axis)
        {
            return axis == Axis2.X ? Axis2.Y : Axis2.X;
        }

        public static Vector2 AsVector2(this Axis2 axis)
        {
            return axis == Axis2.X ? Vector2.right : Vector2.up;
        }

        public static Vector2 JustAxis(this Vector2 vector, Axis2 axis)
        {
            return Vector2.Scale(vector, axis.AsVector2());
        }

        public static float AxisValue(this Vector2 vector, Axis2 axis)
        {
            return axis == Axis2.X ? vector.x : vector.y;
        }

        public static Vector2 WithAxisValue(this Vector2 vector, float value, Axis2 axis)
        {
            var retainedAxis = vector.JustAxis(axis.Other());
            var otherAxis = value * axis.AsVector2();
            return retainedAxis + otherAxis;
        }
    }
}