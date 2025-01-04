using Better.Locators.Runtime;
using Factura.Gameplay.Car;
using Factura.Gameplay.Enemy;
using Factura.Gameplay.Services.Camera;
using Factura.Gameplay.Services.Level;
using Factura.Gameplay.Services.Modules;
using Factura.Gameplay.Tile;
using Factura.UI.Popups.LevelLose;
using Factura.UI.Popups.LevelStart;
using Factura.UI.Popups.LevelWin;
using Factura.UI.Services;
using UnityEngine;
using CameraType = Factura.Gameplay.Services.Camera.CameraType;

namespace Factura.Gameplay
{
    public class GameplayBootstrapper : MonoBehaviour
    {
        [SerializeField] private Transform _vehicleSpawnPoint;
        [SerializeField] private EnemiesSpawnBehaviour _enemiesSpawnBehaviour;
        [SerializeField] private GroundTilesSpawnBehaviour _tilesSpawnBehaviour;

        private LevelService _levelService;
        private PopupService _popupService;
        private ModuleService _moduleService;
        private CameraService _cameraService;

        private TurretBehaviour _turretBehaviour;
        private BulletsPackBehaviour _bulletsPackBehaviour;
        private CarBehaviour _carBehaviour;


        private void Start()
        {
            _moduleService = ServiceLocator.Get<ModuleService>();
            _levelService = ServiceLocator.Get<LevelService>();
            _popupService = ServiceLocator.Get<PopupService>();
            _cameraService = ServiceLocator.Get<CameraService>();

            _cameraService.SetActive(CameraType.PreStartCamera);
            CreateModules();
            InitializeVehicle();
            InitializeSpawners();
            SetTargets();

            _levelService.OnLevelWin += OnLevelWin;
            _levelService.OnLevelLose += OnLevelLose;
            _levelService.FireLevelPreStart();
            _popupService.Show<LevelStartPopupController, LevelStartPopupModel>(new LevelStartPopupModel());
        }

        private void OnDestroy()
        {
            _levelService.OnLevelWin -= OnLevelWin;
            _levelService.OnLevelLose -= OnLevelLose;
        }

        private void SetTargets()
        {
            _tilesSpawnBehaviour.SetTarget(_carBehaviour);
            _enemiesSpawnBehaviour.SetTarget(_carBehaviour);
            _cameraService.SetTarget(_carBehaviour, CameraType.PreStartCamera, false);
            _cameraService.SetTarget(_carBehaviour, CameraType.FollowCamera, true);
        }

        private void InitializeVehicle()
        {
            // _carBehaviour.Initialize();
            _carBehaviour.Attach(_bulletsPackBehaviour);
            _carBehaviour.Attach(_turretBehaviour);
        }

        private void InitializeSpawners()
        {
            _tilesSpawnBehaviour.Initialize();
            _enemiesSpawnBehaviour.Initialize();
        }

        private void CreateModules()
        {
            _carBehaviour = _moduleService.Create<CarBehaviour>(_vehicleSpawnPoint.position);
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