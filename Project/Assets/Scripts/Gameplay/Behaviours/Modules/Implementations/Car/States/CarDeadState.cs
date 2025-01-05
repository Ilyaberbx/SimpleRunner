using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Factura.Gameplay.Services.Module;

namespace Factura.Gameplay.Car.States
{
    public sealed class CarDeadState : BaseCarState
    {
        private readonly VehicleModuleBehaviour _source;

        public CarDeadState(VehicleModuleBehaviour source)
        {
            _source = source;
        }

        public override Task EnterAsync(CancellationToken token)
        {
            var moduleService = ServiceLocator.Get<ModuleService>();
            moduleService.Destroy(_source);
            return Task.CompletedTask;
        }

        public override Task ExitAsync(CancellationToken token)
        {
            return Task.CompletedTask;
        }
    }
}