using System;
using Factura.Gameplay.Tile;
using UnityEngine;

namespace Factura.Gameplay.Services.Tile.Creation
{
    [Serializable]
    public sealed class TilesFactoryConfiguration
    {
        [SerializeField] private TileBehaviour _prefab;

        public TileBehaviour Prefab => _prefab;
    }
}