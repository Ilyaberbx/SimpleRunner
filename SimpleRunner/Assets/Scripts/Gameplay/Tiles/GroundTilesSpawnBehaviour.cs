using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Factura.Gameplay.Services.Level;
using Factura.Gameplay.Services.Tiles;
using Factura.Gameplay.Target;
using UnityEngine;

namespace Factura.Gameplay.Tiles
{
    public sealed class GroundTilesSpawnBehaviour : MonoBehaviour
    {
        private const int TilesRangeCount = 3;

        private TilesService _tilesService;
        private LevelService _levelService;
        private ITarget _target;

        private readonly List<GroundTileBehaviour> _tiles = new();
        private Task _spawningTask;

        private void Start()
        {
            InitializeServices();
            RegisterLevelEvents();
        }

        private void OnDestroy()
        {
            UnregisterLevelEvents();
        }

        public void SetTarget(ITarget target)
        {
            _target = target;
        }

        private void InitializeServices()
        {
            _tilesService = ServiceLocator.Get<TilesService>();
            _levelService = ServiceLocator.Get<LevelService>();
        }

        private void RegisterLevelEvents()
        {
            _levelService.OnLevelPreStart += OnLevelPreStarted;
            _levelService.OnLevelStart += OnLevelStarted;
            _levelService.OnLevelFinish += OnLevelFinished;
        }

        private void UnregisterLevelEvents()
        {
            _levelService.OnLevelPreStart -= OnLevelPreStarted;
            _levelService.OnLevelStart -= OnLevelStarted;
            _levelService.OnLevelFinish -= OnLevelFinished;
        }

        private void OnLevelPreStarted()
        {
            if (_target == null) return;

            var targetPosition = _target.Position;
            var tileSize = _tilesService.TileSize;

            for (var i = 0; i < TilesRangeCount; i++)
            {
                var tilePosition = targetPosition + Vector3.forward * (i * tileSize);
                SpawnTile(tilePosition);
            }
        }

        private void OnLevelStarted()
        {
            _spawningTask = SpawnTilesAsync(destroyCancellationToken);
            _spawningTask.Forget();
        }

        private void OnLevelFinished()
        {
            _spawningTask?.Dispose();
            _spawningTask = null;
        }

        private async Task SpawnTilesAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await Task.Yield();

                if (_target == null) return;

                if (ShouldSpawnNewTile(out var newTilePosition))
                {
                    SpawnTile(newTilePosition);
                }
            }
        }

        private void SpawnTile(Vector3 position)
        {
            var tile = _tilesService.Create(position);
            _tiles.Add(tile);
        }

        private bool ShouldSpawnNewTile(out Vector3 newTilePosition)
        {
            newTilePosition = default;

            if (!TryGetTilePosition(_tiles.Count - TilesRangeCount, out var sourceTilePosition))
                return false;

            var halfTileSize = _tilesService.TileSize / 2;
            var nextTileTriggerPosition = sourceTilePosition + Vector3.forward * halfTileSize;
            var targetPositionZ = _target.Position.z;
            var triggered = Mathf.Abs(targetPositionZ) < Mathf.Abs(nextTileTriggerPosition.z);

            if (!triggered)
                return false;

            if (!TryGetTilePosition(_tiles.Count - 1, out var lastTilePosition))
                return false;

            newTilePosition = lastTilePosition + Vector3.forward * _tilesService.TileSize;
            return true;
        }


        private bool TryGetTilePosition(int index, out Vector3 position)
        {
            if (index < 0 || index >= _tiles.Count)
            {
                position = default;
                return false;
            }

            position = _tiles[index].transform.position;
            return true;
        }
    }
}