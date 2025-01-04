using Better.Commons.Runtime.Extensions;
using UnityEngine;

namespace Factura.Gameplay.LookAt
{
    public sealed class LookAtTargetComponent : ILookAt
    {
        private readonly Transform _source;

        public LookAtTargetComponent(Transform source)
        {
            _source = source;
        }

        public void LookAt(Vector3 target)
        {
            var direction = target - _source.position;
            direction = direction.Flat();
            var angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            _source.rotation = Quaternion.Euler(0, angle, 0);
        }
    }
}