using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using Factura.Gameplay.Modules;
using UnityEngine;

namespace Factura.Gameplay.Services.Modules
{
    [Serializable]
    public sealed class ModuleService : PocoService<ModuleServiceSettings>
    {
        [SerializeField] private Transform _root;

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

        public TModule Create<TModule>() where TModule : BaseModuleBehaviour
        {
            return _moduleFactory.Create<TModule>();
        }
    }
}