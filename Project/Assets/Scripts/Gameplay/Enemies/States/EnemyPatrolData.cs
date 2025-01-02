using Factura.Gameplay.Movement;
using UnityEngine;

namespace Factura.Gameplay.Enemies.States
{
    public sealed class EnemyPatrolData
    {
        public Transform Source { get; }
        public float PatrolRadius { get; }
        public IDynamicMovable DynamicMovable { get; }

        public EnemyPatrolData(Transform source, float patrolRadius, IDynamicMovable dynamicMovable)
        {
            Source = source;
            PatrolRadius = patrolRadius;
            DynamicMovable = dynamicMovable;
        }
    }
}