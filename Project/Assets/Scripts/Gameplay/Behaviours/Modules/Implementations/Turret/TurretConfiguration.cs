using Factura.Gameplay.Projectiles;
using UnityEngine;

namespace Factura.Gameplay
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Turret", fileName = "CarConfiguration", order = 0)]
    public sealed class TurretConfiguration : BaseModuleConfiguration
    {
        [SerializeField] private TurretBehaviour _prefab;
        [SerializeField] private float _fireCooldown;
        [SerializeField] private BaseProjectileBehaviour _projectilePrefab;

        public float FireCooldown => _fireCooldown;
        public BaseProjectileBehaviour ProjectilePrefab => _projectilePrefab;
        public TurretBehaviour Prefab => _prefab;
    }
}