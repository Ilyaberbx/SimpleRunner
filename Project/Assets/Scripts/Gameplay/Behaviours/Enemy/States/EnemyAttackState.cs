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

        protected override void Enter()
        {
            _attack.Process(_targetHealth);
        }

        protected override void Exit()
        {
        }
    }
}