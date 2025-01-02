using Factura.Gameplay.Movement;
using Factura.Gameplay.Target;

namespace Factura.Gameplay.Enemies.States
{
    public sealed class EnemyChaseState : BaseEnemyState
    {
        private readonly IDynamicMovable _dynamicMovable;
        private readonly ITarget _target;
        private readonly MoveState _moveState;

        public EnemyChaseState(IDynamicMovable dynamicMovable, ITarget target)
        {
            _target = target;
            _dynamicMovable = dynamicMovable;
            _moveState = new MoveState(dynamicMovable);
        }

        protected override void Enter()
        {
            _dynamicMovable.SetTarget(_target);
            _moveState.Enter();
        }

        protected override void Exit()
        {
            _moveState.Exit();
        }
    }
}