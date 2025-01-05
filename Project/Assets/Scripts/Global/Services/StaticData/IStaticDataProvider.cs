using Factura.Gameplay;
using Factura.Gameplay.Enemy;
using Factura.Gameplay.Enemy.Spawner;
using Factura.Gameplay.Services.Level;
using Factura.Gameplay.Tile.Spawner;

namespace Factura.Global.Services.StaticData
{
    public interface IStaticDataProvider
    {
        BaseModuleConfiguration GetModuleConfiguration(VehicleModuleType type);
        EnemyConfiguration GetEnemyConfiguration();
        EnemySpawnerConfiguration GetEnemySpawnerConfiguration();
        TileSpawnerConfiguration GetTileSpawnerConfiguration();
        LevelConfiguration GetLevelConfiguration();
    }
}