using Better.Commons.Runtime.Extensions;
using Factura.Gameplay.Projectiles;
using UnityEngine;

namespace Factura.Gameplay.Shooter
{
    public sealed class ProjectileShooter : IShooter
    {
        private const string ProjectileIsNotSetMessage = "ProjectilePrefab is not set.";

        private readonly Transform _shootPoint;
        private readonly BaseProjectileBehaviour _projectilePrefab;

        public ProjectileShooter(Transform shootPoint, BaseProjectileBehaviour projectilePrefab)
        {
            _shootPoint = shootPoint;
            _projectilePrefab = projectilePrefab;
        }

        public void Fire(Vector3 mouseWorldPosition)
        {
            if (_projectilePrefab == null)
            {
                Debug.LogWarning(ProjectileIsNotSetMessage);
                return;
            }

            var shootPosition = _shootPoint.position;
            var projectile = Object.Instantiate(_projectilePrefab, shootPosition, Quaternion.identity);
            shootPosition = shootPosition.Flat();
            mouseWorldPosition = mouseWorldPosition.Flat();
            var direction = shootPosition.DirectionTo(mouseWorldPosition);
            projectile.Initialize(direction);
        }
    }
}