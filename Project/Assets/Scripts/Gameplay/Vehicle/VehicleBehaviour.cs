using System;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Better.StateMachine.Runtime;
using Factura.Gameplay.Damage;
using Factura.Gameplay.Modules;
using Factura.Gameplay.Modules.Locator;
using Factura.Gameplay.Movement;
using Factura.Gameplay.Services.Level;
using Factura.Gameplay.Services.Waypoints;
using Factura.Gameplay.Target;
using Factura.Gameplay.Vehicle.States;
using UnityEngine;

namespace Factura.Gameplay.Vehicle
{
    public sealed class VehicleBehaviour : MonoBehaviour, ITarget, IDamageable
    {
        [SerializeField] private int _health;
        [SerializeField] private MoveByWaypointsConfiguration _moveByWaypointsConfiguration;
        [SerializeField] private LocatorAttachmentConfiguration[] _attachmentConfigurations;

        private LevelService _levelService;
        private WaypointsService _waypointsService;

        private IModulesLocator _locator;
        private IStateMachine<BaseVehicleState> _stateMachine;
        private MoveByWaypointsHandler _movementHandler;
        private DynamicTargetHandler _target;
        private DamageHandler _damageHandler;

        public Vector3 Position => _target.Position;

        public void Initialize()
        {
            _levelService = ServiceLocator.Get<LevelService>();
            _waypointsService = ServiceLocator.Get<WaypointsService>();

            InitializeLocator();
            InitializeStateMachine();
            InitializeHandlers();

            _damageHandler.OnDie += OnDied;
            _levelService.OnLevelStart += OnLevelStarted;
            _levelService.OnLevelFinish += OnLevelFinished;
        }

        private void OnDestroy()
        {
            _damageHandler.OnDie -= OnDied;
            _levelService.OnLevelStart -= OnLevelStarted;
            _levelService.OnLevelFinish -= OnLevelFinished;
        }

        public void Attach(BaseModuleBehaviour moduleBehaviour)
        {
            _locator.Attach(moduleBehaviour);
        }

        public void TakeDamage(int amount)
        {
            _damageHandler.TakeDamage(amount);
        }

        private void OnDied()
        {
            _levelService.FireLevelFinish();
            var deadState = new VehicleDeadState(gameObject);
            _stateMachine.ChangeStateAsync(deadState, destroyCancellationToken).Forget();
        }

        private void OnLevelFinished()
        {
            if (!_stateMachine.IsRunning)
            {
                return;
            }

            _stateMachine.ChangeStateAsync(new VehicleIdleState(), destroyCancellationToken).Forget();
            _stateMachine.Stop();
        }

        private void OnLevelStarted()
        {
            var moveState = new VehicleMoveState(_movementHandler);
            _stateMachine.ChangeStateAsync(moveState, destroyCancellationToken).Forget();
        }

        private void InitializeHandlers()
        {
            var cachedTransform = transform;
            var waypoints = _waypointsService.GetWaypoints(cachedTransform.position);
            _movementHandler = new MoveByWaypointsHandler(cachedTransform, waypoints, _moveByWaypointsConfiguration);
            _damageHandler = new DamageHandler(_health);
            _target = new DynamicTargetHandler(cachedTransform);
        }

        private void InitializeStateMachine()
        {
            _stateMachine = new StateMachine<BaseVehicleState>();
            _stateMachine.Run();
        }

        private void InitializeLocator()
        {
            var source = new Locator<Type, BaseModuleBehaviour>();
            _locator = new ModulesLocator(source, _attachmentConfigurations);
        }
    }
}