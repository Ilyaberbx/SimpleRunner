using Better.Locators.Runtime;
using Factura.Gameplay.Enemies;
using Factura.Gameplay.Modules;
using Factura.Gameplay.Services.Level;
using Factura.Gameplay.Services.Modules;
using Factura.Gameplay.Tiles;
using Factura.Gameplay.Vehicle;
using UnityEngine;

namespace Factura.Gameplay
{
    public class BootstrapperBehaviour : MonoBehaviour
    {
        [SerializeField] private EnemyBehaviour _enemyBehaviour;
        [SerializeField] private GroundTilesSpawnBehaviour _tilesSpawnBehaviour;
        [SerializeField] private VehicleBehaviour _vehicleBehaviour;

        private TurretBehaviour _turretBehaviour;
        private BulletsPackBehaviour _bulletsPackBehaviour;

        private void Start()
        {
            var moduleService = ServiceLocator.Get<ModuleService>();
            var levelService = ServiceLocator.Get<LevelService>();

            _turretBehaviour = moduleService.Create<TurretBehaviour>();
            _bulletsPackBehaviour = moduleService.Create<BulletsPackBehaviour>();

            _vehicleBehaviour.Attach(_bulletsPackBehaviour);
            _vehicleBehaviour.Attach(_turretBehaviour);

            _tilesSpawnBehaviour.SetTarget(_vehicleBehaviour);
            _enemyBehaviour.SetTarget(_vehicleBehaviour);

            levelService.FireLevelPreStart();
            levelService.FireLevelStart();
        }
    }
}