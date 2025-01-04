using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using Factura.Gameplay.Modules;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Factura.Gameplay.Services.Modules
{
    [Serializable]
    public sealed class ModuleService : PocoService<ModuleServiceSettings>
    {
        private IModuleFactory _moduleFactory;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            _moduleFactory = new ModuleFactory(Settings.FactoryConfiguration);
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public TModule Create<TModule>(Vector3 at = default) where TModule : VehicleModuleBehaviour
        {
            return _moduleFactory.Create<TModule>(at);
        }

        public void Destroy<TModule>(TModule module) where TModule : VehicleModuleBehaviour
        {
            var gameObject = module.gameObject;
            Object.Destroy(gameObject);
        }
    }
}