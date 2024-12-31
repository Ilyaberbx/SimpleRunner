using Better.Locators.Runtime;
using Factura.Gameplay.Modules;
using Factura.Gameplay.Services.Level;
using Factura.Gameplay.Services.Modules;
using Factura.Gameplay.Vehicle;
using UnityEngine;

namespace Factura.Gameplay
{
    public class BootstrapperBehaviour : MonoBehaviour
    {
        [SerializeField] private VehicleBehaviour _vehicleBehaviour;
        private TurretBehaviour _turretBehaviour;
        private BulletsPackBehaviour _bulletsPackBehaviour;

        private void Start()
        {
            var moduleService = ServiceLocator.Get<ModuleService>();
            var gameplayService = ServiceLocator.Get<LevelService>();

            _turretBehaviour = moduleService.Create<TurretBehaviour>();
            _bulletsPackBehaviour = moduleService.Create<BulletsPackBehaviour>();

            _vehicleBehaviour.Attach(_bulletsPackBehaviour);
            _vehicleBehaviour.Attach(_turretBehaviour);
            gameplayService.FireLevelStart();
        }
    }
}