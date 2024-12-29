using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using Gameplay.Vehicle.Modules;

namespace Gameplay.Services
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

        public TModule Create<TModule>() where TModule : BaseModuleBehaviour
        {
            return _moduleFactory.Create<TModule>();
        }
    }
}