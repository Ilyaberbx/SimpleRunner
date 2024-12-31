using UnityEngine;

namespace Factura.Gameplay.Extensions
{
    public static class VectorExtensions
    {
        public static Vector3 AddX(this Vector3 source, float value)
        {
            return new Vector3(source.x + value, source.y, source.z);
        }

        public static Vector3 AddY(this Vector3 source, float value)
        {
            return new Vector3(source.x, source.y + value, source.z);
        }

        public static Vector3 AddZ(this Vector3 source, float value)
        {
            return new Vector3(source.x, source.y, source.z + value);
        }
    }
}