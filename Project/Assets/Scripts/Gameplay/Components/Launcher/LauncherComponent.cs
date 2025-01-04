using Factura.Gameplay.Extensions;
using Factura.Gameplay.LookAt;
using Factura.Gameplay.Shoot;
using UnityEngine;

namespace Factura.Gameplay.Launcher
{
    public sealed class LauncherComponent : ILauncher
    {
        private const float ForwardCompensateValue = 100;

        private readonly UnityEngine.Camera _camera;
        private readonly float _fireCoolDown;
        private readonly ILookAt _lookAt;
        private readonly IShooter _shooter;
        private float _timeSinceLastFire;

        public LauncherComponent(UnityEngine.Camera camera, float fireCoolDown, ILookAt lookAt, IShooter shooter)
        {
            _camera = camera;
            _fireCoolDown = fireCoolDown;
            _lookAt = lookAt;
            _shooter = shooter;
            _timeSinceLastFire = _fireCoolDown;
        }

        public void Launch(float deltaTime, Vector3 mousePosition)
        {
            var compensatedMousePosition = mousePosition.AddZ(ForwardCompensateValue);
            var mouseWorldPosition = _camera.ScreenToWorldPoint(compensatedMousePosition);

            _lookAt.LookAt(mouseWorldPosition);

            if (!TryProcessCooldown(deltaTime))
            {
                return;
            }

            _shooter.Shot(mouseWorldPosition);
            _timeSinceLastFire = 0f;
        }

        private bool TryProcessCooldown(float deltaTime)
        {
            _timeSinceLastFire += deltaTime;
            return _timeSinceLastFire >= _fireCoolDown;
        }
    }
}