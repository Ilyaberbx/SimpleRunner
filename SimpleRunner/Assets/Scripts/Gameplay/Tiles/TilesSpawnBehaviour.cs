using System.Collections.Generic;
using Better.Locators.Runtime;
using Gameplay.Services;
using Gameplay.Services.Level;
using UnityEngine;

namespace Gameplay.Tiles
{
    public sealed class TilesSpawnBehaviour : MonoBehaviour
    {
        [SerializeField] private float _tilesOffset;
        [SerializeField] private GroundTileBehaviour _prefab;

        private LevelService _levelService;
        private readonly Queue<GroundTileBehaviour> _tilesQueue = new();

        private void Start()
        {
            _levelService = ServiceLocator.Get<LevelService>();
            _levelService.OnLevelStart += OnLevelStarted;
        }

        private void OnLevelStarted()
        {
        }

        private void OnDestroy()
        {
            _levelService.OnLevelStart -= OnLevelStarted;
        }
    }
}