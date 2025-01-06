using System;

namespace Factura.Gameplay.Enemy.Stickman
{
    public sealed class StickmanSpawnerFactory : IDisposable
    {
        private StickmanSpawnerConfiguration _configuration;

        public StickmanSpawnerFactory(StickmanSpawnerConfiguration configuration)
        {
            _configuration = configuration;
        }

        public StickmanSpawner Create()
        {
            var enemySpawner = new StickmanSpawner(_configuration.WidthOffset,
                _configuration.EnemiesPreTile,
                _configuration.ThresholdSpawnTileIndex);

            return enemySpawner;
        }

        public void Dispose()
        {
            _configuration = null;
        }
    }
}