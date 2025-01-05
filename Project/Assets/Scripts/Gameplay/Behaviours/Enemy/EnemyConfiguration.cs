using Factura.Gameplay.Movement.Target;
using UnityEngine;

namespace Factura.Gameplay.Enemy
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Enemy", fileName = "EnemyConfiguration", order = 0)]
    public sealed class EnemyConfiguration : ScriptableObject
    {
        [SerializeField] private EnemyBehaviour _prefab;
        [SerializeField] private int _healthAmount;
        [SerializeField] private int _damage;
        [SerializeField] private float _patrolRadius;
        [SerializeField] private MoveToTargetConfiguration _chaseMovementConfiguration;
        [SerializeField] private MoveToTargetConfiguration _patrolMovementConfiguration;

        public int HealthAmount => _healthAmount;
        public int Damage => _damage;
        public float PatrolRadius => _patrolRadius;
        public MoveToTargetConfiguration ChaseMovementConfiguration => _chaseMovementConfiguration;
        public MoveToTargetConfiguration PatrolMovementConfiguration => _patrolMovementConfiguration;
        public EnemyBehaviour Prefab => _prefab;
    }
}