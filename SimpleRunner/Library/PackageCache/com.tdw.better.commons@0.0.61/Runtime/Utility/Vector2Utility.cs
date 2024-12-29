using System.Collections.Generic;
using Better.Commons.Runtime.Enums;
using Better.Commons.Runtime.Extensions;
using UnityEngine;

namespace Better.Commons.Runtime.Utility
{
    public struct Vector2Utility
    {
        public static bool Approximately(Vector2 current, Vector2 other)
        {
            return Mathf.Approximately(current.x, other.x) &&
                   Mathf.Approximately(current.y, other.y);
        }

        public static Vector2 MiddlePoint(Vector2 start, Vector2 end)
        {
            var t = start + end;
            return t / 2;
        }

        public static Vector2 MiddlePoint(Vector2 start, Vector2 end, Vector2 offset)
        {
            var middlePoint = MiddlePoint(start, end);
            return middlePoint + offset;
        }

        public static Vector2 SlerpUnclamped(Vector2 a, Vector2 b, float t)
        {
            var dot = Vector2.Dot(a.normalized, b.normalized);
            dot = Mathf.Clamp(dot, -1.0f, 1.0f);
            var theta = Mathf.Acos(dot) * t;
            var relativeVector = (b - a * dot).normalized;

            return a * Mathf.Cos(theta) + relativeVector * Mathf.Sin(theta);
        }

        public static Vector2 Slerp(Vector2 a, Vector2 b, float t)
        {
            t = Mathf.Clamp01(t);
            return SlerpUnclamped(a, b, t);
        }

        public static Vector2 AxesInverseLerp(Vector2 a, Vector2 b, Vector2 value)
        {
            return new Vector2(
                Mathf.InverseLerp(a.x, b.x, value.x),
                Mathf.InverseLerp(a.y, b.y, value.y)
            );
        }

        public static float InverseLerp(Vector2 a, Vector2 b, Vector2 value)
        {
            if (a == b)
            {
                return default;
            }

            var ab = b - a;
            var av = value - a;

            var result = Vector2.Dot(av, ab) / Vector2.Dot(ab, ab);
            return Mathf.Clamp01(result);
        }

        public static Vector2 Direction(Vector2 from, Vector2 to)
        {
            var difference = to - from;
            return difference.normalized;
        }

        public static float SqrDistanceTo(Vector2 from, Vector2 to)
        {
            var difference = to - from;
            return difference.sqrMagnitude;
        }

        public static Vector2 Abs(Vector2 source)
        {
            source.x = Mathf.Abs(source.x);
            source.y = Mathf.Abs(source.y);
            return source;
        }

        public static Vector2 ApplyAxes(Vector2 target, Vector2 source, IEnumerable<Vector2Axis> axes)
        {
            foreach (var axis in axes)
            {
                target[(int)axis] = source.GetAxis(axis);
            }

            return target;
        }
    }
}