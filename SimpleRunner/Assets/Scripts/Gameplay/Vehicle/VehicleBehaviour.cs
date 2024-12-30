using System;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Better.StateMachine.Runtime;
using Gameplay.Services.Level;
using Gameplay.Services.Waypoints;
using Gameplay.Vehicle.Modules;
using Gameplay.Vehicle.Modules.Locator;
using Gameplay.Vehicle.States;
using UnityEngine;

namespace Gameplay.Vehicle
{
    public sealed class VehicleBehaviour : MonoBehaviour
    {
        [SerializeField] private MovementConfiguration _movementConfiguration;
        [SerializeField] private ModuleAttachmentConfiguration[] _attachmentConfiguration;

        private ModulesLocator _locator;
        private StateMachine<BaseVehicleState> _stateMachine;

        private LevelService _levelService;
        private WaypointsService _waypointsService;

        private void Awake()
        {
            InitializeLocator();
            _stateMachine = new StateMachine<BaseVehicleState>();
        }

        private void Start()
        {
            _levelService = ServiceLocator.Get<LevelService>();
            _waypointsService = ServiceLocator.Get<WaypointsService>();
            _levelService.OnLevelStart += OnLevelStarted;
        }

        public void Attach(BaseModuleBehaviour moduleBehaviour)
        {
            _locator.Attach(moduleBehaviour);
        }

        public void Detach(BaseModuleBehaviour moduleBehaviour)
        {
            _locator.Detach(moduleBehaviour);
        }

        private void OnDestroy()
        {
            _levelService.OnLevelStart -= OnLevelStarted;
        }

        private void OnLevelStarted()
        {
            _stateMachine.Run();

            var waypoints = _waypointsService.GetWaypoints(transform.position);
            var destinationSettings = new MoveToDestinationData(transform, _movementConfiguration, waypoints);
            var moveToDestinationState = new MoveToDestinationState(destinationSettings);
            _stateMachine
                .ChangeStateAsync(moveToDestinationState, destroyCancellationToken)
                .Forget();
        }


        private void InitializeLocator()
        {
            var source = new Locator<Type, BaseModuleBehaviour>();
            var settings = new ModulesLocatorSettings(source, _attachmentConfiguration);
            _locator = new ModulesLocator(settings);
        }
    }
}