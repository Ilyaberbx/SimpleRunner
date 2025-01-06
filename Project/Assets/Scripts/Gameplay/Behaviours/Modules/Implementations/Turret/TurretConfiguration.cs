using Factura.Gameplay.Projectiles;
using UnityEngine;

namespace Factura.Gameplay
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/VehicleModules/Turret", fileName = "TurretConfiguration", order = 0)]
    public sealed class TurretConfiguration : BaseModuleConfiguration
    {
        [SerializeField] private TurretBehaviour _prefab;
        [Range(0f,5f)]
        [SerializeField] private float _fireCooldown;
        [SerializeField] private ProjectileConfiguration _projectileConfiguration;
        
        public float FireCooldown => _fireCooldown;
        public TurretBehaviour Prefab => _prefab;
        public ProjectileConfiguration ProjectileConfiguration => _projectileConfiguration;
    }
}