using UnityEngine;

namespace Factura.Gameplay.Enemy.Stickman
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Spawners/Enemy", fileName = "EnemySpawnerConfiguration", order = 0)]
    public sealed class StickmanSpawnerConfiguration : ScriptableObject
    {
        [SerializeField] private float _widthOffset;
        [SerializeField] private float _enemiesPreTile;
        [SerializeField] private int _thresholdSpawnTileIndex;
        public float WidthOffset => _widthOffset;
        public float EnemiesPreTile => _enemiesPreTile;
        public int ThresholdSpawnTileIndex => _thresholdSpawnTileIndex;
    }
}