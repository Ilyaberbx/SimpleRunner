using Factura.Gameplay;
using Factura.Gameplay.Enemy;
using Factura.Gameplay.Enemy.Spawner;
using Factura.Gameplay.Services.Level;
using Factura.Gameplay.Services.Tile.Creation;
using Factura.Gameplay.Services.Waypoints;
using Factura.Gameplay.Tile.Spawner;

namespace Factura.Global.Services.StaticData
{
    public interface IGameplayStaticDataProvider
    {
        BaseModuleConfiguration GetModuleConfiguration(VehicleModuleType type);
        EnemyConfiguration GetEnemyConfiguration();
        EnemySpawnerConfiguration GetEnemySpawnerConfiguration();
        TileSpawnerConfiguration GetTileSpawnerConfiguration();
        TileConfiguration GetTileConfiguration();
        LevelConfiguration GetLevelConfiguration();
        WaypointsConfiguration GetWaypointsConfiguration();
    }
}