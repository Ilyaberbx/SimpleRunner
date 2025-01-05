using Factura.Gameplay.Services.Module;
using UnityEngine;

namespace Factura.Gameplay
{
    public abstract class VehicleModuleFactory : IVehicleModuleFactory
    {
        private readonly BaseModuleConfiguration _configuration;

        protected VehicleModuleFactory(BaseModuleConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected void Initialize(VehicleModuleBehaviour moduleBehaviour)
        {
            moduleBehaviour.Initialize(_configuration.VehicleModuleType);
        }

        public abstract VehicleModuleBehaviour Create(Vector3 at);
    }
}