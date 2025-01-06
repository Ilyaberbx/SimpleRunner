using UnityEngine;

namespace Factura.Gameplay.Projectiles
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Projectiles/Projectile", fileName = "ProjectileConfiguration",
        order = 0)]
    public sealed class ProjectileConfiguration : ScriptableObject
    {
        [SerializeField] private ProjectileBehaviour _prefab;
        [Range(0f, 100f)] [SerializeField] private float _moveSpeed;
        [Range(0f, 10f)] [SerializeField] private float _lifeTime;
        [Range(0, 100)] [SerializeField] private int _damage;

        public ProjectileBehaviour Prefab => _prefab;
        public float MoveSpeed => _moveSpeed;
        public float LifeTime => _lifeTime;
        public int Damage => _damage;
    }
}