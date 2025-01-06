using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Factura.Gameplay.Services.Module;

namespace Factura.Gameplay.Car.States
{
    public sealed class CarDeadState : BaseCarState
    {
        private readonly IReadOnlyList<VehicleModuleBehaviour> _modules;

        public CarDeadState(IReadOnlyList<VehicleModuleBehaviour> modules)
        {
            _modules = modules;
        }

        public override Task EnterAsync(CancellationToken token)
        {
            var moduleService = ServiceLocator.Get<VehicleModuleService>();

            foreach (var module in _modules)
            {
                moduleService.Release(module);
            }

            return Task.CompletedTask;
        }

        public override Task ExitAsync(CancellationToken token)
        {
            return Task.CompletedTask;
        }
    }
}