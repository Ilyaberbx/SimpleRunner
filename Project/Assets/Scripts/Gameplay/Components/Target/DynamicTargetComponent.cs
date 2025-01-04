using UnityEngine;

namespace Factura.Gameplay.Target
{
    public sealed class DynamicTargetComponent : ITarget
    {
        private readonly Transform _transform;

        public Vector3 Position => _transform.position;

        public DynamicTargetComponent(Transform transform)
        {
            _transform = transform;
        }
    }
}