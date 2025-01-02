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
        public event Action OnDie
        {
            add => _damageable.OnDie += value;
            remove => _damageable.OnDie -= value;
        }

        [SerializeField] private int _health;
        [SerializeField] private MoveByWaypointsConfiguration _moveByWaypointsConfiguration;
        [SerializeField] private LocatorAttachmentConfiguration[] _attachmentConfigurations;

        private LevelService _levelService;
        private WaypointsService _waypointsService;

        private IModulesLocator _locator;
        private IStateMachine<BaseVehicleState> _stateMachine;
        private IMovable _movable;
        private ITarget _target;
        private IDamageable _damageable;

        public Vector3 Position => _target.Position;

        public void Initialize()
        {
            _levelService = ServiceLocator.Get<LevelService>();
            _waypointsService = ServiceLocator.Get<WaypointsService>();

            InitializeLocator();
            InitializeStateMachine();
            InitializeHandlers();

            _damageable.OnDie += OnDied;
            _levelService.OnLevelStart += OnLevelStarted;
        }

        private void Start()
        {
        }

        private void OnDestroy()
        {
            _damageable.OnDie -= OnDied;
            _levelService.OnLevelStart -= OnLevelStarted;
        }

        public void Attach(BaseModuleBehaviour moduleBehaviour)
        {
            _locator.Attach(moduleBehaviour);
        }

        public void TakeDamage(int amount)
        {
            _damageable.TakeDamage(amount);
        }

        private void OnDied()
        {
            _levelService.FireLevelFinish();
            var deadState = new VehicleDeadState(gameObject);
            _stateMachine.ChangeStateAsync(deadState, destroyCancellationToken).Forget();
        }

        private void OnLevelStarted()
        {
            var moveState = new VehicleMoveState(_movable);
            _stateMachine.ChangeStateAsync(moveState, destroyCancellationToken).Forget();
        }

        private void InitializeHandlers()
        {
            var cachedTransform = transform;
            var waypoints = _waypointsService.GetWaypoints(cachedTransform.position);
            _movable = new MoveByWaypointsHandler(cachedTransform, waypoints, _moveByWaypointsConfiguration);
            _damageable = new DamageHandler(_health);
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