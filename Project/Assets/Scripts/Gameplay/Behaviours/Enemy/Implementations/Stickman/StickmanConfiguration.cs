using Factura.Gameplay.Movement.Target;
using UnityEngine;

namespace Factura.Gameplay.Enemy.Stickman
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Stickman", fileName = "EnemyConfiguration", order = 0)]
    public sealed class StickmanConfiguration : ScriptableObject
    {
        [SerializeField] private StickmanBehaviour _prefab;
        [SerializeField] private int _healthAmount;
        [SerializeField] private int _damage;
        [SerializeField] private float _patrolRadius;
        [SerializeField] private MoveToTargetConfiguration _chaseMovementConfiguration;
        [SerializeField] private MoveToTargetConfiguration _patrolMovementConfiguration;
        [SerializeField] private float _lookAtSpeed;

        public StickmanBehaviour Prefab => _prefab;
        public int HealthAmount => _healthAmount;
        public int Damage => _damage;
        public float PatrolRadius => _patrolRadius;
        public MoveToTargetConfiguration ChaseMovementConfiguration => _chaseMovementConfiguration;
        public MoveToTargetConfiguration PatrolMovementConfiguration => _patrolMovementConfiguration;
        public float LookAtSpeed => _lookAtSpeed;
    }
}