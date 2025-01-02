using System;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Better.StateMachine.Runtime;
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
        [SerializeField] private float _patrolRange;
        [SerializeField] private TargetTriggerObserver _targetTriggerObserver;
        [SerializeField] private MoveToTargetConfiguration moveToTargetConfiguration;

        private LevelService _levelService;

        private ITarget _target;
        private IDynamicMovable _dynamicMovable;
        private IStateMachine<BaseEnemyState> _stateMachine;
        private EnemyChaseState _chaseState;

        private void Start()
        {
            InitializeStateMachine();
            InitializeHandlers();
            _levelService = ServiceLocator.Get<LevelService>();
            _levelService.OnLevelStart += OnLevelStarted;
            _targetTriggerObserver.OnEnter += OnTargetEntered;
        }

        private void OnDestroy()
        {
            _levelService.OnLevelStart -= OnLevelStarted;
            _targetTriggerObserver.OnEnter -= OnTargetEntered;

            if (_chaseState != null)
            {
                _chaseState.OnChaseComplete -= OnChaseCompleted;
            }
        }

        private void Update()
        {
            _chaseState?.Update();
        }

        public void SetTarget(ITarget target)
        {
            _target = target;
        }

        private void OnLevelStarted()
        {
            if (_target == null)
            {
                return;
            }

            var patrolState = new EnemyPatrolState(transform, _patrolRange, _dynamicMovable);
            _stateMachine.ChangeStateAsync(patrolState, destroyCancellationToken).Forget();
        }

        private void OnTargetEntered(ITarget target)
        {
            _chaseState = new EnemyChaseState(transform, _dynamicMovable, target);
            _chaseState.OnChaseComplete += OnChaseCompleted;
            _stateMachine.ChangeStateAsync(_chaseState, destroyCancellationToken);
        }

        private void OnChaseCompleted()
        {
            var deadState = new EnemyDeadState(gameObject);
            _stateMachine.ChangeStateAsync(deadState, destroyCancellationToken);
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