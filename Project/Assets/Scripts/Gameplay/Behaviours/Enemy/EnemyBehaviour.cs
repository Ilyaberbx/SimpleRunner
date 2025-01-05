using Better.Locators.Runtime;
using Better.StateMachine.Runtime;
using Factura.Gameplay.Attack;
using Factura.Gameplay.Car;
using Factura.Gameplay.Enemy.States;
using Factura.Gameplay.Health;
using Factura.Gameplay.Movement;
using Factura.Gameplay.Services.Level;
using Factura.Gameplay.Target;
using Factura.Gameplay.Triggers;
using Factura.Gameplay.Visitors;
using UnityEngine;

namespace Factura.Gameplay.Enemy
{
    public sealed class EnemyBehaviour : MonoBehaviour, IProjectileVisitable, IEnemyVisitor
    {
        [SerializeField] private TargetTriggerObserver _targetTriggerObserver;
        [SerializeField] private EnemyVisitableTriggerObserver _visitableTriggerObserver;

        private LevelService _levelService;

        private ITarget _target;
        private IStateMachine<BaseEnemyState> _stateMachine;
        private IHealth _health;
        private IDynamicMovable _movement;
        private IAttack _attack;

        public void Initialize(
            IHealth health,
            IStateMachine<BaseEnemyState> stateMachine,
            IDynamicMovable movement,
            IAttack attack)

        {
            _attack = attack;
            _health = health;
            _stateMachine = stateMachine;
            _movement = movement;
            _stateMachine.Run();

            _levelService = ServiceLocator.Get<LevelService>();

            _levelService.OnLevelStart += OnLevelStarted;
            _levelService.OnLevelFinish += OnLevelFinished;
            _targetTriggerObserver.OnEnter += OnTargetEntered;
            _visitableTriggerObserver.OnEnter += OnVisitableEntered;
            _health.OnDie += OnDied;
        }

        private void OnDestroy()
        {
            _levelService.OnLevelStart -= OnLevelStarted;
            _levelService.OnLevelFinish -= OnLevelFinished;
            _targetTriggerObserver.OnEnter -= OnTargetEntered;
            _visitableTriggerObserver.OnEnter -= OnVisitableEntered;
        }

        public void TakeDamage(int amount)
        {
            _health.TakeDamage(amount);
        }

        public void Accept(IProjectileVisitor visitor)
        {
            visitor.Visit(this);
        }

        public void SetTarget(ITarget target)
        {
            _target = target;
        }

        private void OnVisitableEntered(IEnemyVisitable visitable)
        {
            SetDeadState();
            visitable.Accept(this);
        }

        private void OnDied()
        {
            SetDeadState();
        }

        private async void SetDeadState()
        {
            var deadState = new EnemyDeadState(gameObject);
            await _stateMachine.ChangeStateAsync(deadState, destroyCancellationToken);
        }

        private void OnLevelStarted()
        {
            if (_target == null)
            {
                return;
            }

            // var patrolData = new EnemyPatrolData(transform, _patrolRadius, _movement);
            // var patrolState = new EnemyPatrolState(patrolData);
            // _stateMachine.ChangeStateAsync(patrolState, destroyCancellationToken).Forget();
        }

        private async void OnLevelFinished()
        {
            if (!_stateMachine.IsRunning || _stateMachine.CurrentState is EnemyDeadState)
            {
                return;
            }

            await _stateMachine.ChangeStateAsync(new EnemyIdleState(), destroyCancellationToken);
            _stateMachine.Stop();
        }

        private async void OnTargetEntered(ITarget target)
        {
            var chaseState = new EnemyChaseState(_movement, target);
            await _stateMachine.ChangeStateAsync(chaseState, destroyCancellationToken);
        }

        public async void Visit(CarBehaviour carBehaviour)
        {
            var attackState = new EnemyAttackState(carBehaviour.Health, _attack);
            await _stateMachine.ChangeStateAsync(attackState, destroyCancellationToken);
        }
    }
}