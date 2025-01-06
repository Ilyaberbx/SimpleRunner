using System.Threading;
using System.Threading.Tasks;

namespace Factura.Gameplay.States
{
    public sealed class TurretIdleState : BaseTurretState
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