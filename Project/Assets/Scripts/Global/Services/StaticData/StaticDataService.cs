using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Better.Services.Runtime;
using Factura.Gameplay;
using Factura.Gameplay.Enemy;
using Factura.Gameplay.Enemy.Spawner;
using Factura.Gameplay.Services.Level;
using Factura.Gameplay.Tile.Spawner;
using Factura.Global.Services.AssetsManagement;

namespace Factura.Global.Services.StaticData
{
    public sealed class StaticDataService : PocoService, IStaticDataProvider
    {
        private IAssetsProvider _assetsProvider;

        private IReadOnlyDictionary<VehicleModuleType, BaseModuleConfiguration> _moduleConfigurationsMap;
        private LevelConfiguration _levelConfiguration;
        private EnemyConfiguration _enemyConfiguration;
        private EnemySpawnerConfiguration _enemySpawnerConfiguration;
        private TileSpawnerConfiguration _tileSpawnerConfiguration;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override async Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _assetsProvider = ServiceLocator.Get<ResourcesProviderService>();
            _moduleConfigurationsMap = await LoadModulesConfiguration();
            _levelConfiguration = await LoadLevelConfiguration();
            _enemyConfiguration = await LoadEnemyConfiguration();
            _enemySpawnerConfiguration = await LoadEnemySpawnConfiguration();
            _tileSpawnerConfiguration = await LoadTileSpawnerConfiguration();
        }

        #region Configurations Loading

        private Task<TileSpawnerConfiguration> LoadTileSpawnerConfiguration()
        {
            return _assetsProvider.Load<TileSpawnerConfiguration>(StaticDataAddresses.TileSpawner);
        }

        private Task<EnemySpawnerConfiguration> LoadEnemySpawnConfiguration()
        {
            return _assetsProvider.Load<EnemySpawnerConfiguration>(StaticDataAddresses.EnemySpawner);
        }

        private Task<EnemyConfiguration> LoadEnemyConfiguration()
        {
            return _assetsProvider.Load<EnemyConfiguration>(StaticDataAddresses.Enemy);
        }

        private Task<LevelConfiguration> LoadLevelConfiguration()
        {
            return _assetsProvider.Load<LevelConfiguration>(StaticDataAddresses.Level);
        }

        private async Task<IReadOnlyDictionary<VehicleModuleType, BaseModuleConfiguration>> LoadModulesConfiguration()
        {
            var configs = await _assetsProvider
                .LoadAll<BaseModuleConfiguration>(StaticDataAddresses.Modules);

            return configs.ToDictionary(temp => temp.VehicleModuleType);
        }

        #endregion


        public BaseModuleConfiguration GetModuleConfiguration(VehicleModuleType type) =>
            _moduleConfigurationsMap.GetValueOrDefault(type);
        public EnemyConfiguration GetEnemyConfiguration() => _enemyConfiguration;
        public EnemySpawnerConfiguration GetEnemySpawnerConfiguration() => _enemySpawnerConfiguration;

        public TileSpawnerConfiguration GetTileSpawnerConfiguration() => _tileSpawnerConfiguration;
        public LevelConfiguration GetLevelConfiguration() => _levelConfiguration;
    }
}