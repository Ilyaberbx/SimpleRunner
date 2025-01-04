using System.Threading;
using System.Threading.Tasks;

namespace Factura.Gameplay.Vehicle.States
{
    public sealed class VehicleIdleState : BaseVehicleState
    {
        public override Task EnterAsync(CancellationToken token)
        {
            return Task.CompletedTask;
        }

        public override Task ExitAsync(CancellationToken token)
        {
            return Task.CompletedTask;
        }
    }
}