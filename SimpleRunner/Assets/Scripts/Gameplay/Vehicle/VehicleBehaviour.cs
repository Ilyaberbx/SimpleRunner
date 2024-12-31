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
using Factura.Gameplay.Vehicle.States;
using UnityEngine;

namespace Factura.Gameplay.Vehicle
{
    public sealed class VehicleBehaviour : MonoBehaviour, IMovable
    {
        [SerializeField] private WaypointsMovementConfiguration _waypointsMovementConfiguration;
        [SerializeField] private LocatorAttachmentConfiguration[] _attachmentConfigurations;

        private ModulesLocator _locator;
        private StateMachine<BaseVehicleState> _stateMachine;

        private LevelService _levelService;
        private WaypointsService _waypointsService;
        private IMovable _movable;

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

        public Tween MoveTween(Vector3[] waypoints)
        {
            return _movable.MoveTween(waypoints);
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
            _movable = new WaypointsMovementHandler(transform, _waypointsMovementConfiguration);
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