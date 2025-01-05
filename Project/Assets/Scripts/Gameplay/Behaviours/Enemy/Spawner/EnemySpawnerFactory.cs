using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Factura.Gameplay.Enemy.Spawner
{
    public sealed class EnemySpawnerFactory : IDisposable
    {
        private EnemySpawnerConfiguration _configuration;

        public EnemySpawnerFactory(EnemySpawnerConfiguration configuration)
        {
            _configuration = configuration;
        }

        public EnemySpawner Create()
        {
            var enemySpawner = new EnemySpawner(_configuration.WidthOffset, _configuration.EnemiesPreTile,
                _configuration.ThresholdSpawnTileIndex);

            return enemySpawner;
        }

        public void Dispose()
        {
            _configuration = null;
        }
    }
}