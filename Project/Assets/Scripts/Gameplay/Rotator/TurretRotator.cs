using Better.Commons.Runtime.Extensions;
using UnityEngine;

namespace Factura.Gameplay.Rotator
{
    public sealed class TurretRotator : IRotator
    {
        private readonly Transform _source;

        public TurretRotator(Transform source)
        {
            _source = source;
        }

        public void RotateTo(Vector3 mouseWorldPosition)
        {
            var direction = mouseWorldPosition - _source.position;
            direction = direction.Flat();
            var angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            _source.rotation = Quaternion.Euler(0, angle, 0);
        }
    }
}