using System.Threading;
using System.Threading.Tasks;

namespace Factura.Gameplay.Car.States
{
    public sealed class CarIdleState : BaseCarState
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