using System;
using Factura.Gameplay.Tiles;
using UnityEngine;

namespace Factura.Gameplay.Services.Tiles.Creation
{
    [Serializable]
    public sealed class TilesFactoryConfiguration
    {
        [SerializeField] private GroundTileBehaviour _prefab;

        public GroundTileBehaviour Prefab => _prefab;
    }
}