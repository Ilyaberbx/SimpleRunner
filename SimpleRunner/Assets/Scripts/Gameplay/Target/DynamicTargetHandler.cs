using UnityEngine;

namespace Factura.Gameplay.Target
{
    public sealed class DynamicTargetHandler : ITarget
    {
        private readonly Transform _transform;

        public Vector3 Position => _transform.position;

        public DynamicTargetHandler(Transform transform)
        {
            _transform = transform;
        }
    }
}