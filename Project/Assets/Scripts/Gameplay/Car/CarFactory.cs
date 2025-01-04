using System;
using Better.Locators.Runtime;
using Better.StateMachine.Runtime;
using Factura.Gameplay.Attachment;
using Factura.Gameplay.Camera;
using Factura.Gameplay.Car.States;
using Factura.Gameplay.Conditions;
using Factura.Gameplay.Health;
using Factura.Gameplay.Modules;
using Factura.Gameplay.Modules.Locator;
using Factura.Gameplay.Movement.Waypoints;
using Factura.Gameplay.Services.Modules;
using Factura.Gameplay.Services.Waypoints;
using Factura.Gameplay.Target;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Factura.Gameplay.Car
{
    public sealed class CarFactory : IModuleFactory
    {
        private readonly CarConfiguration _configuration;
        private readonly IWaypointsProvider _waypointsProvider;
        private readonly CarBehaviour _prefab;

        public CarFactory(CarConfiguration configuration,
            IWaypointsProvider waypointsProvider,
            CarBehaviour prefab)
        {
            _configuration = configuration;
            _waypointsProvider = waypointsProvider;
            _prefab = prefab;
        }

        public BaseModuleBehaviour Create(Vector3 at)
        {
            var carBehaviour = Object.Instantiate(_prefab, at, Quaternion.identity, null);
            var carTransform = carBehaviour.transform;
            var waypoints = _waypointsProvider.GetWaypoints(at);

            var locator = InitializeLocator();
            var health = new HealthComponent(_configuration.HealthAmount);
            var stateMachine = new StateMachine<BaseCarState>();
            var movement = new MoveByWaypointsComponent(carTransform, waypoints, _configuration.MovementConfiguration);
            var attachment = new ImmediateAttachmentComponent(carTransform, new ValueCondition(true));
            var target = new DynamicTargetComponent(carTransform);
            var cameraTarget = new CameraTargetComponent();

            carBehaviour.Initialize(
                locator,
                health,
                stateMachine,
                movement,
                attachment,
                target,
                cameraTarget);

            return carBehaviour;
        }

        private VehicleModulesLocator InitializeLocator()
        {
            var attachmentConfiguration = _configuration.AttachmentConfiguration;
            return new VehicleModulesLocator(new Locator<Type, BaseModuleBehaviour>(), attachmentConfiguration);
        }
    }
}