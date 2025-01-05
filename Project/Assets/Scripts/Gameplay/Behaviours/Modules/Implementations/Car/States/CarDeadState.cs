using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Factura.Gameplay.Services.Module;

namespace Factura.Gameplay.Car.States
{
    public sealed class CarDeadState : BaseCarState
    {
        private readonly CarBehaviour _context;

        public CarDeadState(CarBehaviour context)
        {
            _context = context;
        }

        public override Task EnterAsync(CancellationToken token)
        {
            var moduleService = ServiceLocator.Get<VehicleModuleService>();
            moduleService.Destroy(_context);
            return Task.CompletedTask;
        }

        public override Task ExitAsync(CancellationToken token)
        {
            return Task.CompletedTask;
        }
    }
}