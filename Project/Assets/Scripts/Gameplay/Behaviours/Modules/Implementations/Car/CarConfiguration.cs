using Factura.Gameplay.Movement.Waypoints;
using UnityEngine;

namespace Factura.Gameplay.Car
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/VehicleModules/Car", fileName = "CarConfiguration", order = 0)]
    public sealed class CarConfiguration : BaseModuleConfiguration
    {
        [SerializeField] private CarBehaviour _prefab;
        [Range(0, 100)] [SerializeField] private int _healthAmount;
        [SerializeField] private MoveByWaypointsConfiguration _movementConfiguration;

        public int HealthAmount => _healthAmount;
        public MoveByWaypointsConfiguration MovementConfiguration => _movementConfiguration;
        public CarBehaviour Prefab => _prefab;
    }
}