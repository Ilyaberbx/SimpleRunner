using Factura.Gameplay.Attachment;
using UnityEngine;

namespace Factura.Gameplay.BulletsPack
{
    public sealed class BulletsPackFactory : VehicleModuleFactory
    {
        private readonly BulletsPackConfiguration _configuration;

        public BulletsPackFactory(BulletsPackConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public override VehicleModuleBehaviour Create(Vector3 at)
        {
            var prefab = _configuration.Prefab;
            var bulletsPackBehaviour = Object.Instantiate(prefab, at, Quaternion.identity, null);
            Initialize(bulletsPackBehaviour);

            var bulletsPackTransform = bulletsPackBehaviour.transform;
            var attachment = new ImmediateAttachmentComponent(bulletsPackTransform);
            bulletsPackBehaviour.Initialize(attachment);
            return bulletsPackBehaviour;
        }
    }
}