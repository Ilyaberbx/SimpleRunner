using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Factura.Gameplay.Patrol;

namespace Factura.Gameplay.Enemy.States
{
    public sealed class EnemyPatrolState : BaseEnemyState
    {
        private readonly IPatrol _patrol;

        public EnemyPatrolState(IPatrol patrol)
        {
            _patrol = patrol;
        }

        public override Task EnterAsync(CancellationToken token)
        {
            _patrol.ConductAsync(token).Forget();
            return Task.CompletedTask;
        }

        public override Task ExitAsync(CancellationToken token)
        {
            _patrol.Stop();
            return Task.CompletedTask;
        }
    }
}