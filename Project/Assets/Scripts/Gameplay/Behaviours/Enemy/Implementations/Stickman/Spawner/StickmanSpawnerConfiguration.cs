using UnityEngine;

namespace Factura.Gameplay.Enemy.Stickman
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Spawners/Enemy", fileName = "EnemySpawnerConfiguration", order = 0)]
    public sealed class StickmanSpawnerConfiguration : ScriptableObject
    {
        [Range(0f, 100f)] [SerializeField] private float _widthOffset;
        [Range(1f, 100f)] [SerializeField] private float _enemiesPreTile;
        [Min(0)] [SerializeField] private int _thresholdSpawnTileIndex;
        public float WidthOffset => _widthOffset;
        public float EnemiesPreTile => _enemiesPreTile;
        public int ThresholdSpawnTileIndex => _thresholdSpawnTileIndex;
    }
}