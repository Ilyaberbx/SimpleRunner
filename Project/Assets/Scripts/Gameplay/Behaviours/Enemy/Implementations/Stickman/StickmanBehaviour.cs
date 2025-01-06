using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Better.StateMachine.Runtime;
using Factura.Gameplay.Animations;
using Factura.Gameplay.Attack;
using Factura.Gameplay.Car;
using Factura.Gameplay.Health;
using Factura.Gameplay.LookAt;
using Factura.Gameplay.Movement;
using Factura.Gameplay.Patrol;
using Factura.Gameplay.Services.Enemy;
using Factura.Gameplay.Services.Level;
using Factura.Gameplay.Target;
using Factura.Gameplay.Triggers;
using Factura.Gameplay.Visitors;
using UnityEngine;

namespace Factura.Gameplay.Enemy.Stickman
{
    public sealed class StickmanBehaviour : BaseEnemyBehaviour, IProjectileVisitable, IEnemyVisitor
    {
        [SerializeField] private Animator _sourceAnimator;
        [SerializeField] private TargetTriggerObserver _targetTriggerObserver;
        [SerializeField] private EnemyVisitableTriggerObserver _visitableTriggerObserver;
        [SerializeField] private StickmanAnimationEventsObserver _animationEventsObserver;

        private LevelService _levelService;

        private ITarget _target;
        private IStateMachine<BaseStickmanState> _stateMachine;
        private IDynamicMovable _chaseMovement;
        private IAttack _attack;
        private IPatrol _patrol;
        private IStickmanAnimator _animator;
        private ILookAt _chaseLookAt;
        private ILookAt _patrolLookAt;
        private EnemyService _enemyService;

        public IHealth Health { get; private set; }
        public Animator SourceAnimator => _sourceAnimator;
        private bool IsDead => !_stateMachine.IsRunning || _stateMachine.CurrentState is StickmanDeadState;

        public void Initialize(
            ILookAt chaseLookAt,
            ILookAt patrolLookAt,
            IHealth health,
            IStateMachine<BaseStickmanState> stateMachine,
            IDynamicMovable chaseMovement,
            IPatrol patrol,
            IAttack attack,
            IStickmanAnimator animator)

        {
            _chaseLookAt = chaseLookAt;
            _patrolLookAt = patrolLookAt;
            _animator = animator;
            _patrol = patrol;
            _attack = attack;
            Health = health;
            _stateMachine = stateMachine;
            _chaseMovement = chaseMovement;
            _stateMachine.Run();

            _levelService = ServiceLocator.Get<LevelService>();
            _enemyService = ServiceLocator.Get<EnemyService>();

            _levelService.OnLevelStart += OnLevelStarted;
            _levelService.OnLevelFinish += OnLevelFinished;
            _targetTriggerObserver.OnEnter += OnTargetEntered;
            _visitableTriggerObserver.OnEnter += OnVisitableEntered;
            _animationEventsObserver.OnAttack += OnAttacked;
            Health.OnDie += OnDied;
        }

        private void OnDestroy()
        {
            _levelService.OnLevelStart -= OnLevelStarted;
            _levelService.OnLevelFinish -= OnLevelFinished;
            _targetTriggerObserver.OnEnter -= OnTargetEntered;
            _visitableTriggerObserver.OnEnter -= OnVisitableEntered;
            _animationEventsObserver.OnAttack -= OnAttacked;
            Health.OnDie -= OnDied;
        }

        public void Accept(IProjectileVisitor visitor)
        {
            visitor.Visit(this);
        }

        public void SetTarget(ITarget target)
        {
            if (IsDead)
            {
                return;
            }

            _target = target;

            if (_levelService.IsLevelStarted)
            {
                SetPatrolState();
            }
        }

        public void Visit(CarBehaviour carBehaviour)
        {
            if (IsDead)
            {
                return;
            }

            var attackState =
                new StickmanAttackState(_animationEventsObserver, _animator, carBehaviour.Health, _attack);

            _stateMachine
                .ChangeStateAsync(attackState, destroyCancellationToken)
                .Forget();
        }

        private void OnVisitableEntered(IEnemyVisitable visitable)
        {
            visitable.Accept(this);
        }

        private void OnAttacked()
        {
            if (IsDead)
            {
                return;
            }

            SetDeadState();
        }

        private void OnDied()
        {
            if (IsDead)
            {
                return;
            }

            SetDeadState();
        }

        private void OnLevelStarted()
        {
            SetPatrolState();
        }

        private async void OnLevelFinished()
        {
            if (IsDead)
            {
                return;
            }

            await _stateMachine.ChangeStateAsync(new StickmanIdleState(_animator), destroyCancellationToken);
            _stateMachine.Stop();
        }

        private void OnTargetEntered(ITarget target)
        {
            if (IsDead)
            {
                return;
            }

            var chaseState = new StickmanChaseState(_chaseLookAt, _animator, target, _chaseMovement);
            _stateMachine.ChangeStateAsync(chaseState, destroyCancellationToken).Forget();
        }

        private void SetDeadState()
        {
            if (IsDead)
            {
                return;
            }

            var deadState = new StickmanDeadState(this);
            _stateMachine.ChangeStateAsync(deadState, destroyCancellationToken).Forget();
        }

        private void SetPatrolState()
        {
            if (_target == null)
            {
                return;
            }

            var patrolState = new StickmanPatrolState(_patrolLookAt, _animator, _patrol);
            _stateMachine.ChangeStateAsync(patrolState, destroyCancellationToken).Forget();
        }
    }
}