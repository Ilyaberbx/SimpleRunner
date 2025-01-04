using UnityEngine;

namespace Factura.Gameplay.Target
{
    public class StaticTargetComponent : ITarget
    {
        public Vector3 Position { get; }

        public StaticTargetComponent(Vector3 position)
        {
            Position = position;
        }
    }
}