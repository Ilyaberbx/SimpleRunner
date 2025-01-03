using Better.Locators.Runtime;
using Factura.Gameplay.Enemies;
using Factura.Gameplay.Modules;
using Factura.Gameplay.Services.Level;
using Factura.Gameplay.Services.Modules;
using Factura.Gameplay.Tiles;
using Factura.Gameplay.Vehicle;
using Factura.UI.Popups.LevelLose;
using Factura.UI.Popups.LevelStart;
using Factura.UI.Popups.LevelWin;
using Factura.UI.Services;
using UnityEngine;

namespace Factura.Gameplay
{
    public class GameplayBootstrapper : MonoBehaviour
    {
        [SerializeField] private EnemiesSpawnBehaviour _enemiesSpawnBehaviour;
        [SerializeField] private GroundTilesSpawnBehaviour _tilesSpawnBehaviour;
        [SerializeField] private VehicleBehaviour _vehicleBehaviour;

        private TurretBehaviour _turretBehaviour;
        private BulletsPackBehaviour _bulletsPackBehaviour;
        private LevelService _levelService;
        private PopupService _popupService;
        private ModuleService _moduleService;

        private void Start()
        {
            _moduleService = ServiceLocator.Get<ModuleService>();
            _levelService = ServiceLocator.Get<LevelService>();
            _popupService = ServiceLocator.Get<PopupService>();

            CreateModules();
            InitializeVehicle();
            InitializeSpawners();
            SetTargets();

            _levelService.OnLevelWin += OnLevelWin;
            _levelService.OnLevelLose += OnLevelLose;
            _levelService.FireLevelPreStart();
            _popupService.Show<LevelStartPopupController, LevelStartModel>(new LevelStartModel());
        }

        private void OnDestroy()
        {
            _levelService.OnLevelWin -= OnLevelWin;
            _levelService.OnLevelLose -= OnLevelLose;
        }

        private void SetTargets()
        {
            _tilesSpawnBehaviour.SetTarget(_vehicleBehaviour);
            _enemiesSpawnBehaviour.SetTarget(_vehicleBehaviour);
        }

        private void InitializeVehicle()
        {
            _vehicleBehaviour.Initialize();
            _vehicleBehaviour.Attach(_bulletsPackBehaviour);
            _vehicleBehaviour.Attach(_turretBehaviour);
        }

        private void InitializeSpawners()
        {
            _tilesSpawnBehaviour.Initialize();
            _enemiesSpawnBehaviour.Initialize();
        }

        private void CreateModules()
        {
            _turretBehaviour = _moduleService.Create<TurretBehaviour>();
            _bulletsPackBehaviour = _moduleService.Create<BulletsPackBehaviour>();
        }

        private void OnLevelLose()
        {
            _popupService.Show<LevelLosePopupController, LevelLosePopupModel>(new LevelLosePopupModel());
        }

        private void OnLevelWin()
        {
            _popupService.Show<LevelWinPopupController, LevelWinPopupModel>(new LevelWinPopupModel());
        }
    }
}