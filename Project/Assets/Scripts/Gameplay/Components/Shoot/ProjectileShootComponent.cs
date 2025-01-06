using Better.Commons.Runtime.Extensions;
using Factura.Gameplay.Projectiles;
using UnityEngine;

namespace Factura.Gameplay.Shoot
{
    public sealed class ProjectileShootComponent : IShooter
    {
        private const string ProjectileIsNotSetMessage = "ProjectilePrefab is not set.";

        private readonly Transform _shootPoint;
        private readonly ProjectileFactory _projectileFactory;

        public ProjectileShootComponent(Transform shootPoint, ProjectileConfiguration projectileConfiguration)
        {
            _shootPoint = shootPoint;
            _projectileFactory = new ProjectileFactory(projectileConfiguration);
        }

        public void Shot(Vector3 mouseWorldPosition)
        {
            var shootPosition = _shootPoint.position;
            var projectileBehaviour = _projectileFactory.Create(shootPosition);
            shootPosition = shootPosition.Flat();
            mouseWorldPosition = mouseWorldPosition.Flat();
            var direction = shootPosition.DirectionTo(mouseWorldPosition);
            projectileBehaviour.Fire(direction);
        }
    }
}