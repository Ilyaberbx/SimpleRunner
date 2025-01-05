using Factura.Gameplay.Attachment;
using Factura.Gameplay.Services.Module;
using UnityEngine;

namespace Factura.Gameplay.BulletsPack
{
    public sealed class BulletsPackFactory : IModuleFactory
    {
        private readonly BulletsPackConfiguration _configuration;

        public BulletsPackFactory(BulletsPackConfiguration configuration)
        {
            _configuration = configuration;
        }

        public VehicleModuleBehaviour Create(Vector3 at)
        {
            var prefab = _configuration.Prefab;
            var bulletsPackBehaviour = Object.Instantiate(prefab, at, Quaternion.identity, null);
            var bulletsPackTransform = bulletsPackBehaviour.transform;

            var attachment = new ImmediateAttachmentComponent(bulletsPackTransform);

            bulletsPackBehaviour.Initialize(attachment);
            return bulletsPackBehaviour;
        }
    }
}