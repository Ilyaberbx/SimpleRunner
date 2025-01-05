using Better.Locators.Runtime;
using Better.StateMachine.Runtime;
using Factura.Gameplay.Attachment;
using Factura.Gameplay.Car.States;
using Factura.Gameplay.Health;
using Factura.Gameplay.ModulesLocator;
using Factura.Gameplay.Movement.Waypoints;
using Factura.Gameplay.Services.Waypoints;
using Factura.Gameplay.Target;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Factura.Gameplay.Car
{
    public sealed class CarFactory : VehicleModuleFactory
    {
        private readonly CarConfiguration _configuration;
        private readonly IWaypointsProvider _waypointsProvider;

        public CarFactory(CarConfiguration configuration,
            IWaypointsProvider waypointsProvider) : base(configuration)
        {
            _configuration = configuration;
            _waypointsProvider = waypointsProvider;
        }

        public override VehicleModuleBehaviour Create(Vector3 at)
        {
            var prefab = _configuration.Prefab;
            var carBehaviour = Object.Instantiate(prefab, at, Quaternion.identity, null);
            Initialize(carBehaviour);

            var waypoints = _waypointsProvider.GetWaypoints(at);
            var carTransform = carBehaviour.transform;
            var locator = new VehicleModulesLocator(new Locator<VehicleModuleType, VehicleModuleBehaviour>());
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
    }
}