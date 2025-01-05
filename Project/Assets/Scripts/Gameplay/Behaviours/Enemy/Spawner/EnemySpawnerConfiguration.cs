using UnityEngine;

namespace Factura.Gameplay.Enemy.Spawner
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Spawners/Enemy", fileName = "EnemySpawnConfiguration", order = 0)]
    public sealed class EnemySpawnerConfiguration : ScriptableObject
    {
        [SerializeField] private float _widthOffset;
        [SerializeField] private float _enemiesPreTile;
        [SerializeField] private int _thresholdSpawnTileIndex;
        public float WidthOffset => _widthOffset;
        public float EnemiesPreTile => _enemiesPreTile;
        public int ThresholdSpawnTileIndex => _thresholdSpawnTileIndex;
    }
}