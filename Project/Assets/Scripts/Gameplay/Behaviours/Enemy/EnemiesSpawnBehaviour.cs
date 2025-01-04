using Better.Locators.Runtime;
using Factura.Gameplay.Extensions;
using Factura.Gameplay.Services.Enemies;
using Factura.Gameplay.Services.Tiles;
using Factura.Gameplay.Target;
using Factura.Gameplay.Tile;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Factura.Gameplay.Enemy
{
    public sealed class EnemiesSpawnBehaviour : MonoBehaviour
    {
        private const float WidthOffset = 68;
        private const float EnemiesPerTile = 10;

        private TileService _tileService;
        private EnemyService _enemyService;
        private ITarget _target;

        private Bounds TileBounds => _tileService.TileBounds;

        public void Initialize()
        {
            _tileService = ServiceLocator.Get<TileService>();
            _enemyService = ServiceLocator.Get<EnemyService>();

            _tileService.OnTileCreate += OnTileCreated;
        }

        private void OnDestroy()
        {
            _tileService.OnTileCreate -= OnTileCreated;
        }

        public void SetTarget(ITarget target)
        {
            _target = target;
        }

        private void OnTileCreated(GroundTileBehaviour tile, int index)
        {
            if (index == 0)
            {
                return;
            }

            if (_target == null)
            {
                return;
            }

            var tilePosition = tile.transform.position;

            for (var i = 0; i < EnemiesPerTile; i++)
            {
                var minBounds = Vector3.zero.AddX(TileBounds.min.x + WidthOffset).AddZ(TileBounds.min.z);
                var maxBounds = Vector3.zero.AddX(TileBounds.max.x - WidthOffset).AddZ(TileBounds.max.z);

                var randomX = Random.Range(minBounds.x, maxBounds.x);
                var randomZ = Random.Range(minBounds.z, maxBounds.z);
                var at = tilePosition.AddX(randomX).AddZ(randomZ);

                var enemyBehaviour = _enemyService.CreateEnemy(at);
                enemyBehaviour.SetTarget(_target);
            }
        }
    }
}