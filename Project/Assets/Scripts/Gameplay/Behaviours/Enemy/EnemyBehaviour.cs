using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Better.StateMachine.Runtime;
using Factura.Gameplay.Attack;
using Factura.Gameplay.Car;
using Factura.Gameplay.Enemy.States;
using Factura.Gameplay.Health;
using Factura.Gameplay.Movement;
using Factura.Gameplay.Patrol;
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
        private IDynamicMovable _chaseMovement;
        private IAttack _attack;
        private IPatrol _patrol;

        public IHealth Health { get; private set; }

        public void Initialize(
            IHealth health,
            IStateMachine<BaseEnemyState> stateMachine,
            IDynamicMovable chaseMovement,
            IPatrol patrol,
            IAttack attack)

        {
            _patrol = patrol;
            _attack = attack;
            Health = health;
            _stateMachine = stateMachine;
            _chaseMovement = chaseMovement;
            _stateMachine.Run();

            _levelService = ServiceLocator.Get<LevelService>();

            _levelService.OnLevelStart += OnLevelStarted;
            _levelService.OnLevelFinish += OnLevelFinished;
            _targetTriggerObserver.OnEnter += OnTargetEntered;
            _visitableTriggerObserver.OnEnter += OnVisitableEntered;
            Health.OnDie += OnDied;
        }

        private void OnDestroy()
        {
            _levelService.OnLevelStart -= OnLevelStarted;
            _levelService.OnLevelFinish -= OnLevelFinished;
            _targetTriggerObserver.OnEnter -= OnTargetEntered;
            _visitableTriggerObserver.OnEnter -= OnVisitableEntered;
            Health.OnDie -= OnDied;
        }

        public void Accept(IProjectileVisitor visitor)
        {
            visitor.Visit(this);
        }

        public void SetTarget(ITarget target)
        {
            _target = target;

            if (_levelService.IsLevelStarted)
            {
                SetPatrolState();
            }
        }

        public Task Visit(CarBehaviour carBehaviour)
        {
            var attackState = new EnemyAttackState(carBehaviour.Health, _attack);
            return _stateMachine.ChangeStateAsync(attackState, destroyCancellationToken);
        }

        private async void OnVisitableEntered(IEnemyVisitable visitable)
        {
            await visitable.Accept(this);
            SetDeadState();
        }

        private void OnDied()
        {
            SetDeadState();
        }

        private void OnLevelStarted()
        {
            SetPatrolState();
        }

        private async void OnLevelFinished()
        {
            if (!_stateMachine.IsRunning || _stateMachine.CurrentState is EnemyDeadState or EnemyAttackState)
            {
                return;
            }

            await _stateMachine.ChangeStateAsync(new EnemyIdleState(), destroyCancellationToken);
            _stateMachine.Stop();
        }

        private void OnTargetEntered(ITarget target)
        {
            var chaseState = new EnemyChaseState(target, _chaseMovement);
            _stateMachine.ChangeStateAsync(chaseState, destroyCancellationToken).Forget();
        }

        private async void SetDeadState()
        {
            if (!_stateMachine.IsRunning)
            {
                return;
            }

            var deadState = new EnemyDeadState(gameObject);
            await _stateMachine.ChangeStateAsync(deadState, destroyCancellationToken);
            _stateMachine.Stop();
        }

        private void SetPatrolState()
        {
            if (_target == null)
            {
                return;
            }

            var patrolState = new EnemyPatrolState(_patrol);
            _stateMachine.ChangeStateAsync(patrolState, destroyCancellationToken).Forget();
        }
    }
}