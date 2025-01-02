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

        private TileService _tileService;
        private LevelService _levelService;
        private ITarget _target;

        private readonly List<GroundTileBehaviour> _tiles = new();
        private Task _spawningTask;
        private CancellationTokenSource _tokenSource;

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
            _tileService = ServiceLocator.Get<TileService>();
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
            var tileLength = _tileService.TileLength;

            for (var i = 0; i < TilesRangeCount; i++)
            {
                var tilePosition = targetPosition + Vector3.forward * (i * tileLength);
                SpawnTile(tilePosition);
            }
        }

        private void OnLevelStarted()
        {
            _tokenSource = CancellationTokenSource.CreateLinkedTokenSource(destroyCancellationToken);
            _spawningTask = SpawnTilesAsync(_tokenSource.Token);
            _spawningTask.Forget();
        }

        private void OnLevelFinished()
        {
            _tokenSource.Cancel();
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
            var tile = _tileService.CreateTile(position);
            _tiles.Add(tile);
        }

        private bool ShouldSpawnNewTile(out Vector3 newTilePosition)
        {
            newTilePosition = default;

            var sourceTileIndex = _tiles.Count - TilesRangeCount;
            if (!TryGetTilePosition(sourceTileIndex, out var sourceTilePosition))
                return false;

            var halfTileLength = _tileService.TileLength / 2;
            var nextTileTriggerPosition = sourceTilePosition + Vector3.forward * halfTileLength;
            var targetPosition = _target.Position;
            var triggered = Mathf.Abs(targetPosition.z) < Mathf.Abs(nextTileTriggerPosition.z);

            if (!triggered)
                return false;

            if (!TryGetTilePosition(_tiles.Count - 1, out var lastTilePosition))
                return false;

            newTilePosition = lastTilePosition + Vector3.forward * _tileService.TileLength;
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