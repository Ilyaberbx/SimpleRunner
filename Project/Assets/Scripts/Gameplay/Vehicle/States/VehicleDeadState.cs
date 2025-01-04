using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Factura.Gameplay.Modules;
using Factura.Gameplay.Services.Modules;

namespace Factura.Gameplay.Vehicle.States
{
    public sealed class VehicleDeadState : BaseVehicleState
    {
        private readonly VehicleModuleBehaviour _source;

        public VehicleDeadState(VehicleBehaviour source)
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