using System.Threading;
using System.Threading.Tasks;
using Factura.Gameplay.Attack;
using Factura.Gameplay.Health;

namespace Factura.Gameplay.Enemy.States
{
    public class EnemyAttackState : BaseEnemyState
    {
        private readonly IHealth _targetHealth;
        private readonly IAttack _attack;

        public EnemyAttackState(IHealth targetHealth, IAttack attack)
        {
            _targetHealth = targetHealth;
            _attack = attack;
        }

        public override Task EnterAsync(CancellationToken token)
        {
            return _attack.ProcessAsync(_targetHealth, token);
        }

        public override Task ExitAsync(CancellationToken token)
        {
            return Task.CompletedTask;
        }
    }
}