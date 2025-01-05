using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Factura.Gameplay.Services.Level;
using Factura.Gameplay.Services.Tile;
using Factura.Gameplay.Target;
using UnityEngine;

namespace Factura.Gameplay.Tile.Spawner
{
    public sealed class TileSpawner : IDisposable
    {
        private readonly TileService _tileService;
        private readonly LevelService _levelService;
        private readonly int _preStartTilesCount;

        private ITarget _target;

        private Task _spawningTask;
        private CancellationTokenSource _finishLevelCancellationSource;
        private readonly CancellationTokenSource _disposeCancellationSource;

        private IReadOnlyList<TileBehaviour> Tiles => _tileService.Tiles;

        public TileSpawner(int preStartTilesCount)
        {
            _disposeCancellationSource = new CancellationTokenSource();
            _preStartTilesCount = preStartTilesCount;
            _tileService = ServiceLocator.Get<TileService>();
            _levelService = ServiceLocator.Get<LevelService>();
            RegisterLevelEvents();
        }

        public void Dispose()
        {
            UnregisterLevelEvents();
            _disposeCancellationSource?.Cancel();
        }

        public void SetTarget(ITarget target)
        {
            _target = target;
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
            var tileLength = _tileService.TileLength;

            for (var i = 0; i < _preStartTilesCount; i++)
            {
                var tilePosition = targetPosition + Vector3.forward * (i * tileLength);
                SpawnTile(tilePosition);
            }
        }

        private void OnLevelStarted()
        {
            _finishLevelCancellationSource = CancellationTokenSource
                .CreateLinkedTokenSource(_disposeCancellationSource.Token);

            _spawningTask = SpawnTilesAsync(_finishLevelCancellationSource.Token);
            _spawningTask.Forget();
        }

        private void OnLevelFinished()
        {
            _finishLevelCancellationSource?.Cancel();
        }

        private async Task SpawnTilesAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await Task.Yield();

                if (token.IsCancellationRequested) return;
                if (_target == null) return;

                if (ShouldSpawnNewTile(out var newTilePosition))
                {
                    SpawnTile(newTilePosition);
                }
            }
        }

        private void SpawnTile(Vector3 position)
        {
            _tileService.CreateTile(position);
        }

        private bool ShouldSpawnNewTile(out Vector3 newTilePosition)
        {
            newTilePosition = default;

            var sourceTileIndex = Tiles.Count - _preStartTilesCount;
            if (!TryGetTilePosition(sourceTileIndex, out var sourceTilePosition))
                return false;

            var halfTileLength = _tileService.TileLength / 2;
            var nextTileTriggerPosition = sourceTilePosition + Vector3.forward * halfTileLength;
            var targetPosition = _target.Position;
            var triggered = Mathf.Abs(targetPosition.z) < Mathf.Abs(nextTileTriggerPosition.z);

            if (!triggered)
                return false;

            if (!TryGetTilePosition(Tiles.Count - 1, out var lastTilePosition))
                return false;

            newTilePosition = lastTilePosition + Vector3.forward * _tileService.TileLength;
            return true;
        }


        private bool TryGetTilePosition(int index, out Vector3 position)
        {
            if (index < 0 || index >= Tiles.Count)
            {
                position = default;
                return false;
            }

            position = Tiles[index].transform.position;
            return true;
        }
    }
}