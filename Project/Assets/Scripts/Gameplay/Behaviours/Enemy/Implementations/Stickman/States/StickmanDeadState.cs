using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Factura.Gameplay.Services.Enemy;

namespace Factura.Gameplay.Enemy.Stickman
{
    public sealed class StickmanDeadState : BaseStickmanState
    {
        private readonly BaseEnemyBehaviour _source;

        public StickmanDeadState(BaseEnemyBehaviour source)
        {
            _source = source;
        }

        public override Task EnterAsync(CancellationToken token)
        {
            var enemyService = ServiceLocator.Get<EnemyService>();
            enemyService.Release(_source);
            return Task.CompletedTask;
        }

        public override Task ExitAsync(CancellationToken token)
        {
            return Task.CompletedTask;
        }
    }
}