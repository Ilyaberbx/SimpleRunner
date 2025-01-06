using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using UnityEngine;

namespace Factura.Gameplay.LookAt
{
    public sealed class ImmediateLookAtComponent : ILookAt
    {
        private readonly Transform _source;

        public ImmediateLookAtComponent(Transform source)
        {
            _source = source;
        }

        public Task LookAt(Vector3 target, CancellationToken token)
        {
            var direction = target - _source.position;
            direction = direction.Flat();
            var angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            _source.rotation = Quaternion.Euler(0, angle, 0);
            return Task.CompletedTask;
        }
    }
}