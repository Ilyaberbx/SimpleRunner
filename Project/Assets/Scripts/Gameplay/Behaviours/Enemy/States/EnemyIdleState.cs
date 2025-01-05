using System.Threading;
using System.Threading.Tasks;

namespace Factura.Gameplay.Enemy.States
{
    public sealed class EnemyIdleState : BaseEnemyState
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