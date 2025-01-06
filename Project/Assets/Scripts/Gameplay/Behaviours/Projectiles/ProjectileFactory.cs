using UnityEngine;

namespace Factura.Gameplay.Projectiles
{
    public sealed class ProjectileFactory
    {
        private readonly ProjectileConfiguration _configuration;

        public ProjectileFactory(ProjectileConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ProjectileBehaviour Create(Vector3 at)
        {
            var prefab = _configuration.Prefab;
            var projectileBehaviour = Object.Instantiate(prefab, at, Quaternion.identity);

            projectileBehaviour.Initialize(_configuration.Damage, _configuration.MoveSpeed, _configuration.LifeTime);
            return projectileBehaviour;
        }
    }
}