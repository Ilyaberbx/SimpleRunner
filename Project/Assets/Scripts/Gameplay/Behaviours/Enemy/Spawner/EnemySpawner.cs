using System;
using Better.Locators.Runtime;
using Factura.Gameplay.Extensions;
using Factura.Gameplay.Services.Enemy;
using Factura.Gameplay.Services.Tile;
using Factura.Gameplay.Target;
using Factura.Gameplay.Tile;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Factura.Gameplay.Enemy.Spawner
{
    public sealed class EnemySpawner : IDisposable
    {
        private readonly TileService _tileService;
        private readonly EnemyService _enemyService;

        private readonly float _widthOffset;
        private readonly float _enemiesPerTile;
        private readonly int _tileIndexThreshold;

        private ITarget _target;
        private Bounds TileBounds => _tileService.TileBounds;

        public EnemySpawner(float widthOffset,
            float enemiesPerTile,
            int tileIndexThreshold)
        {
            _tileIndexThreshold = tileIndexThreshold;
            _widthOffset = widthOffset;
            _enemiesPerTile = enemiesPerTile;

            _tileService = ServiceLocator.Get<TileService>();
            _enemyService = ServiceLocator.Get<EnemyService>();

            _tileService.OnTileCreate += OnTileCreated;
        }

        public void Dispose()
        {
            _tileService.OnTileCreate -= OnTileCreated;
        }

        public void SetTarget(ITarget target)
        {
            _target = target;
        }

        private void OnTileCreated(TileBehaviour tile, int index)
        {
            if (index <= _tileIndexThreshold)
            {
                return;
            }

            if (_target == null)
            {
                return;
            }

            var tilePosition = tile.transform.position;

            for (var i = 0; i < _enemiesPerTile; i++)
            {
                var at = GetEnemySpawnPoint(tilePosition);
                var enemyBehaviour = _enemyService.CreateEnemy(at);
                enemyBehaviour.SetTarget(_target);
            }
        }

        private Vector3 GetEnemySpawnPoint(Vector3 tilePosition)
        {
            var minBounds = Vector3.zero.AddX(TileBounds.min.x + _widthOffset).AddZ(TileBounds.min.z);
            var maxBounds = Vector3.zero.AddX(TileBounds.max.x - _widthOffset).AddZ(TileBounds.max.z);

            var randomX = Random.Range(minBounds.x, maxBounds.x);
            var randomZ = Random.Range(minBounds.z, maxBounds.z);
            var at = tilePosition.AddX(randomX).AddZ(randomZ);
            return at;
        }
    }
}