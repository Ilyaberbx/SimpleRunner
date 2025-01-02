using Better.Commons.Runtime.Extensions;
using Factura.Gameplay.Extensions;
using UnityEngine;

namespace Factura.Gameplay.Launcher
{
    public sealed class TurretLauncher : ILauncher
    {
        private const float ForwardCompensateValue = 100;

        private readonly Transform _source;
        private readonly Camera _camera;
        private readonly TurretLauncherConfiguration _configuration;

        public TurretLauncher(Transform source, Camera camera, TurretLauncherConfiguration configuration)
        {
            _source = source;
            _camera = camera;
            _configuration = configuration;
        }

        public void Launch(float deltaTime, Vector3 mousePosition)
        {
            RotateTurretToMousePosition(mousePosition);
        }

        private void RotateTurretToMousePosition(Vector3 mousePosition)
        {
            var mouseWorldPosition = _camera.ScreenToWorldPoint(mousePosition.AddZ(ForwardCompensateValue));
            var direction = mouseWorldPosition - _source.position;
            direction = direction.Flat();
            var angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            _source.rotation = Quaternion.Euler(0, angle, 0);
        }
    }
}