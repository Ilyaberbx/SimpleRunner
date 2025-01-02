using System;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Better.StateMachine.Runtime;
using DG.Tweening;
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
    public sealed class VehicleBehaviour : MonoBehaviour, ITarget
    {
        [SerializeField] private WaypointsMovementConfiguration _waypointsMovementConfiguration;
        [SerializeField] private LocatorAttachmentConfiguration[] _attachmentConfigurations;

        private ModulesLocator _locator;
        private StateMachine<BaseVehicleState> _stateMachine;

        private LevelService _levelService;
        private WaypointsService _waypointsService;
        private IMovable _movable;
        private ITarget _target;

        public Vector3 Position => _target.Position;

        private void Awake()
        {
            InitializeLocator();
            InitializeStateMachine();
            InitializeHandlers();
        }

        private void Start()
        {
            _levelService = ServiceLocator.Get<LevelService>();
            _waypointsService = ServiceLocator.Get<WaypointsService>();
            _levelService.OnLevelStart += OnLevelStarted;
        }

        private void OnDestroy()
        {
            _levelService.OnLevelStart -= OnLevelStarted;
        }

        public void Attach(BaseModuleBehaviour moduleBehaviour)
        {
            _locator.Attach(moduleBehaviour);
        }

        private void OnLevelStarted()
        {
            var waypoints = _waypointsService.GetWaypoints(transform.position);
            var moveToDestinationState = new MoveToDestinationState(_movable, waypoints);

            _stateMachine.ChangeStateAsync(moveToDestinationState, destroyCancellationToken)
                .Forget();
        }

        private void InitializeHandlers()
        {
            var cachedTransform = transform;
            _movable = new WaypointsMovementHandler(cachedTransform, _waypointsMovementConfiguration);
            _target = new TargetHandler(cachedTransform);
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