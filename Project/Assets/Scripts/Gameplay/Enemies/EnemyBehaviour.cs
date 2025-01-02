using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Better.StateMachine.Runtime;
using Factura.Gameplay.Damage;
using Factura.Gameplay.Enemies.States;
using Factura.Gameplay.Movement;
using Factura.Gameplay.Services.Level;
using Factura.Gameplay.Target;
using Factura.Gameplay.Triggers;
using UnityEngine;

namespace Factura.Gameplay.Enemies
{
    public sealed class EnemyBehaviour : MonoBehaviour
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _patrolRadius;
        [SerializeField] private TargetTriggerObserver _targetTriggerObserver;
        [SerializeField] private DamageableTriggerObserver _damageableTriggerObserver;
        [SerializeField] private MoveToTargetConfiguration moveToTargetConfiguration;

        private LevelService _levelService;

        private ITarget _target;
        private IDynamicMovable _dynamicMovable;
        private IStateMachine<BaseEnemyState> _stateMachine;

        private void Start()
        {
            InitializeStateMachine();
            InitializeHandlers();
            _levelService = ServiceLocator.Get<LevelService>();
            _levelService.OnLevelStart += OnLevelStarted;
            _targetTriggerObserver.OnEnter += OnTargetEntered;
            _damageableTriggerObserver.OnEnter += OnDamageableEntered;
        }

        private void OnDestroy()
        {
            _levelService.OnLevelStart -= OnLevelStarted;
            _targetTriggerObserver.OnEnter -= OnTargetEntered;
            _damageableTriggerObserver.OnEnter -= OnDamageableEntered;
        }

        public void SetTarget(ITarget target)
        {
            _target = target;
        }

        private void OnDamageableEntered(IDamageable damageable)
        {
            damageable.TakeDamage(_damage);
            var deadState = new EnemyDeadState(gameObject);
            _stateMachine.ChangeStateAsync(deadState, destroyCancellationToken);
        }

        private void OnLevelStarted()
        {
            if (_target == null)
            {
                return;
            }

            var patrolData = new EnemyPatrolData(transform, _patrolRadius, _dynamicMovable);
            var patrolState = new EnemyPatrolState(patrolData);
            _stateMachine.ChangeStateAsync(patrolState, destroyCancellationToken).Forget();
        }

        private void OnTargetEntered(ITarget target)
        {
             var chaseState = new EnemyChaseState(_dynamicMovable, target);
            _stateMachine.ChangeStateAsync(chaseState, destroyCancellationToken);
        }


        private void InitializeStateMachine()
        {
            _stateMachine = new StateMachine<BaseEnemyState>();
            _stateMachine.Run();
        }

        private void InitializeHandlers()
        {
            _dynamicMovable = new MoveToTargetHandler(transform, moveToTargetConfiguration);
        }
    }
}