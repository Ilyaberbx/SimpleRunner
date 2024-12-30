using Better.Locators.Runtime;
using Gameplay.Services;
using Gameplay.Services.Modules;
using Gameplay.Vehicle;
using Gameplay.Vehicle.Modules;
using UnityEngine;

namespace Gameplay
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
            gameplayService.FireStartLevel();
        }

        private void OnDestroy()
        {
            _vehicleBehaviour.Detach(_turretBehaviour);
            _vehicleBehaviour.Detach(_bulletsPackBehaviour);
        }
    }
}