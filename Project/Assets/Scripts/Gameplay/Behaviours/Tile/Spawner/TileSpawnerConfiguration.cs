using UnityEngine;

namespace Factura.Gameplay.Tile.Spawner
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Spawners/Tile", fileName = "TileSpawnerConfiguration", order = 0)]
    public sealed class TileSpawnerConfiguration : ScriptableObject
    {
        [Min(1)] [SerializeField] private int _preStartTilesCount;

        public int PreStartTilesCount => _preStartTilesCount;
    }
}