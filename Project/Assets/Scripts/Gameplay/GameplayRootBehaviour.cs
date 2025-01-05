using Better.Locators.Runtime;
using Factura.Gameplay.BulletsPack;
using Factura.Gameplay.Car;
using Factura.Gameplay.Enemy.Spawner;
using Factura.Gameplay.Services.Camera;
using Factura.Gameplay.Services.Level;
using Factura.Gameplay.Services.Module;
using Factura.Gameplay.Services.Waypoints;
using Factura.Gameplay.Tile.Spawner;
using Factura.Global.Services.StaticData;
using Factura.UI.Popups.LevelLose;
using Factura.UI.Popups.LevelStart;
using Factura.UI.Services;
using UnityEngine;
using CameraType = Factura.Gameplay.Services.Camera.CameraType;

namespace Factura.Gameplay
{
    public class GameplayRootBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform _carSpawnPoint;

        private LevelService _levelService;
        private PopupService _popupService;
        private ModuleService _moduleService;
        private CameraService _cameraService;
        private WaypointsService _waypointsService;
        private IGameplayStaticDataProvider _gameplayStaticDataProvider;

        private TurretBehaviour _turretBehaviour;
        private BulletsPackBehaviour _bulletsPackBehaviour;
        private CarBehaviour _carBehaviour;
        private EnemySpawner _enemySpawner;

        private TileSpawner _tilesSpawner;

        private void Start()
        {
            _gameplayStaticDataProvider = ServiceLocator.Get<GameplayStaticDataService>();
            _moduleService = ServiceLocator.Get<ModuleService>();
            _levelService = ServiceLocator.Get<LevelService>();
            _waypointsService = ServiceLocator.Get<WaypointsService>();
            _popupService = ServiceLocator.Get<PopupService>();
            _cameraService = ServiceLocator.Get<CameraService>();
            _cameraService.SetActive(CameraType.PreStartCamera);

            RegisterFactories();
            CreateModules();
            InitializeVehicleModules();
            InitializeSpawners();
            SetCarAsTarget();

            _levelService.OnLevelWin += OnLevelWin;
            _levelService.OnLevelLose += OnLevelLose;
            _levelService.FireLevelPreStart();
            _popupService.Show<LevelStartPopupController, LevelStartPopupModel>(PopupType.LevelStart,
                new LevelStartPopupModel());
        }

        private void OnDestroy()
        {
            _levelService.OnLevelWin -= OnLevelWin;
            _levelService.OnLevelLose -= OnLevelLose;
            _moduleService.UnregisterAllFactories();
            DisposeSpawners();
        }

        private void InitializeVehicleModules()
        {
            RegisterFactories();
            CreateModules();
            AttachModules();
        }

        private void AttachModules()
        {
            var modulesLocator = _carBehaviour.ModulesLocator;
            modulesLocator.Attach(_bulletsPackBehaviour);
            modulesLocator.Attach(_turretBehaviour);
        }

        private void InitializeSpawners()
        {
            var enemySpawnerConfiguration = _gameplayStaticDataProvider.GetEnemySpawnerConfiguration();
            using var enemySpawnerFactory = new EnemySpawnerFactory(enemySpawnerConfiguration);
            _enemySpawner = enemySpawnerFactory.Create();

            var tileSpawnerConfiguration = _gameplayStaticDataProvider.GetTileSpawnerConfiguration();
            using var tileSpawnerFactory = new TileSpawnerFactory(tileSpawnerConfiguration);
            _tilesSpawner = tileSpawnerFactory.Create();
        }

        private void DisposeSpawners()
        {
            _tilesSpawner.Dispose();
            _enemySpawner.Dispose();
            _tilesSpawner = null;
            _enemySpawner = null;
        }

        private void SetCarAsTarget()
        {
            _tilesSpawner.SetTarget(_carBehaviour.Target);
            _enemySpawner.SetTarget(_carBehaviour.Target);
            _cameraService.SetTarget(_carBehaviour.CameraTarget, CameraType.PreStartCamera, false);
            _cameraService.SetTarget(_carBehaviour.CameraTarget, CameraType.FollowCamera, true);
        }

        private void RegisterFactories()
        {
            var carConfiguration = _moduleService.GetConfiguration<CarConfiguration>();
            var turretConfiguration = _moduleService.GetConfiguration<TurretConfiguration>();
            var bulletsPackConfiguration = _moduleService.GetConfiguration<BulletsPackConfiguration>();
            _moduleService.RegisterFactory<CarBehaviour>(new CarFactory(carConfiguration, _waypointsService));
            _moduleService.RegisterFactory<TurretBehaviour>(new TurretFactory(turretConfiguration, _cameraService));
            _moduleService.RegisterFactory<BulletsPackBehaviour>(new BulletsPackFactory(bulletsPackConfiguration));
        }

        private void CreateModules()
        {
            _carBehaviour = _moduleService.Create<CarBehaviour>(_carSpawnPoint.position);
            _turretBehaviour = _moduleService.Create<TurretBehaviour>();
            _bulletsPackBehaviour = _moduleService.Create<BulletsPackBehaviour>();
        }

        private void OnLevelLose()
        {
            _popupService.Show<LevelLosePopupController, LevelLosePopupModel>(PopupType.LevelLose,
                new LevelLosePopupModel());
        }

        private void OnLevelWin()
        {
            _popupService.Show<LevelLosePopupController, LevelLosePopupModel>(PopupType.LevelWin,
                new LevelLosePopupModel());
        }
    }
}