using Better.StateMachine.Runtime;
using Factura.Gameplay.Attachment;
using Factura.Gameplay.Launcher;
using Factura.Gameplay.LookAt;
using Factura.Gameplay.Services.Camera;
using Factura.Gameplay.Shoot;
using Factura.Gameplay.States;
using UnityEngine;

namespace Factura.Gameplay
{
    public class TurretFactory : VehicleModuleFactory
    {
        private readonly TurretConfiguration _configuration;
        private readonly ICameraProvider _cameraProvider;

        public TurretFactory(TurretConfiguration configuration, ICameraProvider cameraProvider)
            : base(configuration)
        {
            _configuration = configuration;
            _cameraProvider = cameraProvider;
        }

        public override VehicleModuleBehaviour Create(Vector3 at)
        {
            var prefab = _configuration.Prefab;
            var turretBehaviour = Object.Instantiate(prefab, at, Quaternion.identity, null);
            Initialize(turretBehaviour);

            var turretTransform = turretBehaviour.transform;
            var camera = _cameraProvider.MainCamera;
            var shootPoint = turretBehaviour.ShootPoint;
            var attachment = new ImmediateAttachmentComponent(turretTransform);
            var lookAt = new ImmediateLookAtComponent(turretTransform);
            var shoot = new ProjectileShootComponent(shootPoint, _configuration.ProjectileConfiguration);
            var launcher = new LauncherComponent(camera, _configuration.FireCooldown, lookAt, shoot);
            var stateMachine = new StateMachine<BaseTurretState>();

            turretBehaviour.Initialize(
                attachment,
                launcher,
                stateMachine);

            return turretBehaviour;
        }
    }
}