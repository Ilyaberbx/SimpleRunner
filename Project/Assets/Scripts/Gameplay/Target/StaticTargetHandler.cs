using UnityEngine;

namespace Factura.Gameplay.Target
{
    public class StaticTargetHandler : ITarget
    {
        public Vector3 Position { get; }

        public StaticTargetHandler(Vector3 position)
        {
            Position = position;
        }
    }
}