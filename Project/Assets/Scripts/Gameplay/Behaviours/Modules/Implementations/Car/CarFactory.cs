using System;
using Better.Locators.Runtime;
using Better.StateMachine.Runtime;
using Factura.Gameplay.Attachment;
using Factura.Gameplay.Car.States;
using Factura.Gameplay.Health;
using Factura.Gameplay.ModulesLocator;
using Factura.Gameplay.Movement.Waypoints;
using Factura.Gameplay.Services.Module;
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

        public CarFactory(CarConfiguration configuration,
            IWaypointsProvider waypointsProvider)
        {
            _configuration = configuration;
            _waypointsProvider = waypointsProvider;
        }

        public VehicleModuleBehaviour Create(Vector3 at)
        {
            var prefab = _configuration.Prefab;
            var carBehaviour = Object.Instantiate(prefab, at, Quaternion.identity, null);
            var carTransform = carBehaviour.transform;
            var waypoints = _waypointsProvider.GetWaypoints(at);

            var locator = InitializeLocator();
            var health = new HealthComponent(_configuration.HealthAmount);
            var stateMachine = new StateMachine<BaseCarState>();
            var movement = new MoveByWaypointsComponent(carTransform, waypoints, _configuration.MovementConfiguration);
            var attachment = new ImmediateAttachmentComponent(carTransform);
            var target = new DynamicTargetComponent(carTransform);

            carBehaviour.Initialize(
                locator,
                health,
                stateMachine,
                movement,
                attachment,
                target);

            return carBehaviour;
        }

        private VehicleModulesLocator InitializeLocator()
        {
            var attachmentConfiguration = _configuration.AttachmentConfiguration;
            return new VehicleModulesLocator(new Locator<Type, VehicleModuleBehaviour>(), attachmentConfiguration);
        }
    }
}