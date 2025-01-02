using UnityEngine;

namespace Factura.Gameplay.Target
{
    public sealed class TargetHandler : ITarget
    {
        private readonly Transform _transform;

        public Vector3 Position => _transform.position;

        public TargetHandler(Transform transform)
        {
            _transform = transform;
        }
    }
}