using System;

namespace Factura.Gameplay.Tile.Spawner
{
    public sealed class TileSpawnerFactory : IDisposable
    {
        private TileSpawnerConfiguration _configuration;

        public TileSpawnerFactory(TileSpawnerConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TileSpawner Create()
        {
            return new TileSpawner(_configuration.PreStartTilesCount);
        }

        public void Dispose()
        {
            _configuration = null;
        }
    }
}