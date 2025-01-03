using Factura.Gameplay.Extensions;
using Factura.Gameplay.Rotator;
using Factura.Gameplay.Shooter;
using UnityEngine;

namespace Factura.Gameplay.Launcher
{
    public sealed class TurretLauncher : ILauncher
    {
        private const float ForwardCompensateValue = 100;

        private readonly Camera _camera;
        private readonly float _fireCoolDown;
        private readonly IRotator _rotator;
        private readonly IShooter _shooter;
        private float _timeSinceLastFire;

        public TurretLauncher(Camera camera, float fireCoolDown, IRotator rotator, IShooter shooter)
        {
            _camera = camera;
            _fireCoolDown = fireCoolDown;
            _rotator = rotator;
            _shooter = shooter;
            _timeSinceLastFire = _fireCoolDown;
        }

        public void Launch(float deltaTime, Vector3 mousePosition)
        {
            var mouseWorldPosition = _camera.ScreenToWorldPoint(mousePosition.AddZ(ForwardCompensateValue));
            _rotator.RotateTo(mouseWorldPosition);

            _timeSinceLastFire += deltaTime;

            if (_timeSinceLastFire < _fireCoolDown)
            {
                return;
            }

            _shooter.Fire(mouseWorldPosition);
            _timeSinceLastFire = 0f;
        }
    }
}