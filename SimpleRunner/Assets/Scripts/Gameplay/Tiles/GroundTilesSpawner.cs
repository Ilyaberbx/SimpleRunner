using System.Collections.Generic;
using Better.Locators.Runtime;
using Factura.Gameplay.Services.Level;
using Factura.Gameplay.Services.Tiles;
using UnityEngine;

namespace Factura.Gameplay.Tiles
{
    public sealed class GroundTilesSpawner : MonoBehaviour
    {
        [SerializeField] private float _spawnOffSet;

        private TilesService _tilesService;
        private LevelService _levelService;

        private readonly Queue<GroundTileBehaviour> _tilesQueue = new();

        private void Start()
        {
            _tilesService = ServiceLocator.Get<TilesService>();
            _levelService = ServiceLocator.Get<LevelService>();

            _levelService.OnLevelStart += OnLevelStarted;
            _levelService.OnLevelFinish += OnLevelFinished;
        }

        private void OnDestroy()
        {
            _levelService.OnLevelStart -= OnLevelStarted;
            _levelService.OnLevelFinish -= OnLevelFinished;
        }

        private void OnLevelStarted()
        {
            StartSpawning();
        }

        private void OnLevelFinished()
        {
            StopSpawning();
        }

        private void StartSpawning()
        {
        }

        private void Spawn()
        {
            var tileBehaviour = _tilesService.Create(GetTilePosition());
            _tilesQueue.Enqueue(tileBehaviour);
        }

        private Vector3 GetTilePosition()
        {
            return default;
        }

        private void StopSpawning()
        {
        }
    }
}