using UnityEngine;

namespace Factura.Gameplay.Tile.Spawner
{
    public sealed class TileSpawnerConfiguration : ScriptableObject
    {
        [SerializeField] private int _preStartTilesCount;

        public int PreStartTilesCount => _preStartTilesCount;
    }
}