using System;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Better.StateMachine.Runtime;
using Factura.Gameplay.Car;
using Factura.Gameplay.Enemies.States;
using Factura.Gameplay.Health;
using Factura.Gameplay.Movement;
using Factura.Gameplay.Movement.Target;
using Factura.Gameplay.Services.Level;
using Factura.Gameplay.Target;
using Factura.Gameplay.Triggers;
using Factura.Gameplay.Visitors;
using UnityEngine;

namespace Factura.Gameplay.Enemies
{
    public sealed class EnemyBehaviour : MonoBehaviour, IHealth, IProjectileVisitable, IEnemyVisitor
    {
        public event Action OnDie
        {
            add => _health.OnDie += value;
            remove => _health.OnDie -= value;
        }

        [SerializeField] private int _healthCount;

        [SerializeField] private int _damage;

        [SerializeField] private float _patrolRadius;

        [SerializeField] private TargetTriggerObserver _targetTriggerObserver;

        [SerializeField] private EnemyVisitableTriggerObserver _visitableTriggerObserver;

        [SerializeField] private MoveToTargetConfiguration _moveToTargetConfiguration;

        private LevelService _levelService;

        private ITarget _target;
        private IStateMachine<BaseEnemyState> _stateMachine;
        private IHealth _health;
        private IDynamicMovable _movement;

        private void Start()
        {
            InitializeStateMachine();
            InitializeHandlers();
            _levelService = ServiceLocator.Get<LevelService>();
            _levelService.OnLevelStart += OnLevelStarted;
            _levelService.OnLevelFinish += OnLevelFinished;
            _targetTriggerObserver.OnEnter += OnTargetEntered;
            _visitableTriggerObserver.OnEnter += OnDamageableEntered;
            _health.OnDie += OnDied;
        }

        private void OnDestroy()
        {
            _levelService.OnLevelStart -= OnLevelStarted;
            _levelService.OnLevelFinish -= OnLevelFinished;
            _targetTriggerObserver.OnEnter -= OnTargetEntered;
            _visitableTriggerObserver.OnEnter -= OnDamageableEntered;
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

        private void OnDamageableEntered(IEnemyVisitable visitable)
        {
            SetDeadState();
            visitable.Accept(this);
        }

        private void OnDied()
        {
            SetDeadState();
        }

        private void SetDeadState()
        {
            var deadState = new EnemyDeadState(gameObject);
            _stateMachine.ChangeStateAsync(deadState, destroyCancellationToken);
        }

        private void OnLevelStarted()
        {
            if (_target == null)
            {
                return;
            }

            var patrolData = new EnemyPatrolData(transform, _patrolRadius, _movement);
            var patrolState = new EnemyPatrolState(patrolData);
            _stateMachine.ChangeStateAsync(patrolState, destroyCancellationToken).Forget();
        }

        private void OnLevelFinished()
        {
            if (!_stateMachine.IsRunning || _stateMachine.CurrentState is EnemyDeadState)
            {
                return;
            }

            _stateMachine.ChangeStateAsync(new EnemyIdleState(), destroyCancellationToken).Forget();
            _stateMachine.Stop();
        }

        private void OnTargetEntered(ITarget target)
        {
            var chaseState = new EnemyChaseState(_movement, target);
            _stateMachine.ChangeStateAsync(chaseState, destroyCancellationToken);
        }

        private void InitializeStateMachine()
        {
            _stateMachine = new StateMachine<BaseEnemyState>();
            _stateMachine.Run();
        }

        private void InitializeHandlers()
        {
            _movement = new MoveToTargetComponent(transform, _moveToTargetConfiguration);
            _health = new HealthComponent(_healthCount);
        }

        public void Visit(CarBehaviour carBehaviour)
        {
            if (carBehaviour == null)
            {
                return;
            }

            carBehaviour.TakeDamage(_damage);
        }
    }
}