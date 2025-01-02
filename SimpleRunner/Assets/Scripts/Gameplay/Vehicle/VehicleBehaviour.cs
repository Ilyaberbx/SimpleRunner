using System;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Better.StateMachine.Runtime;
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
        [SerializeField] private MoveByWaypointsConfiguration _moveByWaypointsConfiguration;
        [SerializeField] private LocatorAttachmentConfiguration[] _attachmentConfigurations;

        private ModulesLocator _locator;
        private IStateMachine<BaseVehicleState> _stateMachine;

        private LevelService _levelService;
        private WaypointsService _waypointsService;
        private IMovable _movable;
        private ITarget _target;

        public Vector3 Position => _target.Position;

        private void Awake()
        {
            InitializeLocator();
            InitializeStateMachine();
        }

        private void Start()
        {
            _levelService = ServiceLocator.Get<LevelService>();
            _waypointsService = ServiceLocator.Get<WaypointsService>();

            InitializeHandlers();

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
            var moveState = new VehicleMoveState(_movable);
            _stateMachine.ChangeStateAsync(moveState, destroyCancellationToken).Forget();
        }

        private void InitializeHandlers()
        {
            var cachedTransform = transform;
            var waypoints = _waypointsService.GetWaypoints(cachedTransform.position);
            _movable = new MoveByWaypointsHandler(cachedTransform, waypoints, _moveByWaypointsConfiguration);
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