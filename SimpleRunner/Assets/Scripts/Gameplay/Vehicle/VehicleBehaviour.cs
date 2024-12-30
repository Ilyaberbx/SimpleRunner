using System;
using System.Linq;
using Better.Locators.Runtime;
using Better.StateMachine.Runtime;
using Gameplay.Services;
using Gameplay.Vehicle.Modules;
using Gameplay.Vehicle.Modules.Locator;
using Gameplay.Vehicle.States;
using UnityEngine;

namespace Gameplay.Vehicle
{
    public sealed class VehicleBehaviour : MonoBehaviour
    {
        [SerializeField] private MovementConfiguration _movementConfiguration;
        [SerializeField] private ModuleAttachmentConfiguration[] _attachmentData;
        private ModulesLocator _locator;
        private LevelService _levelService;
        private StateMachine<BaseVehicleState> _stateMachine;

        private void Awake()
        {
            InitializeLocator();
            _stateMachine = new StateMachine<BaseVehicleState>();
        }

        private void Start()
        {
            _levelService = ServiceLocator.Get<LevelService>();
            _levelService.OnLevelStarted += OnLevelStarted;
        }

        private void OnDestroy()
        {
            _levelService.OnLevelStarted -= OnLevelStarted;
        }

        private async void OnLevelStarted()
        {
            _stateMachine.Run();
            var moveToDestinationState = CreateMoveToDestinationState();
            await _stateMachine.ChangeStateAsync(moveToDestinationState, destroyCancellationToken);
        }

        private MoveToDestinationState CreateMoveToDestinationState()
        {
            var waypoints = _levelService.GetWaypoints(transform.position);
            var destinationSettings = new MoveToDestinationData(transform, _movementConfiguration, waypoints);
            var moveToDestinationState = new MoveToDestinationState(destinationSettings);
            return moveToDestinationState;
        }

        private void InitializeLocator()
        {
            var source = new Locator<Type, BaseModuleBehaviour>();
            var settings = new ModulesLocatorSettings(source, _attachmentData);
            _locator = new ModulesLocator(settings);
        }

        public void Attach(BaseModuleBehaviour moduleBehaviour)
        {
            _locator.Attach(moduleBehaviour);
        }

        public void Detach(BaseModuleBehaviour moduleBehaviour)
        {
            _locator.Detach(moduleBehaviour);
        }
    }
}