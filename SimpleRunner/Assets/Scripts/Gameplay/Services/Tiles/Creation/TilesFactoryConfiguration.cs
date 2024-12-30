using System;
using Gameplay.Tiles;
using UnityEngine;

namespace Gameplay.Services.Tiles.Creation
{
    [Serializable]
    public sealed class TilesFactoryConfiguration
    {
        [SerializeField] private GroundTileBehaviour _prefab;

        public GroundTileBehaviour Prefab => _prefab;
    }
}