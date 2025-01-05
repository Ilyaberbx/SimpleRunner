using Better.StateMachine.Runtime;
using Factura.Gameplay.Attack;
using Factura.Gameplay.Enemy.States;
using Factura.Gameplay.Health;
using Factura.Gameplay.Movement.Target;
using Factura.Gameplay.Patrol;
using Factura.Gameplay.Services.Enemy;
using Factura.Gameplay.Target;
using UnityEngine;

namespace Factura.Gameplay.Enemy
{
    public sealed class EnemyFactory : IEnemyFactory
    {
        private readonly EnemyConfiguration _configuration;

        public EnemyFactory(EnemyConfiguration configuration)
        {
            _configuration = configuration;
        }

        public EnemyBehaviour Create(Vector3 at, Transform parent)
        {
            var prefab = _configuration.Prefab;
            var enemyBehaviour = Object.Instantiate(prefab, at, Quaternion.identity, parent);
            var enemyTransform = enemyBehaviour.transform;

            var patrolTarget = new StaticTargetComponent(enemyTransform.position);
            var health = new HealthComponent(_configuration.HealthAmount);
            var stateMachine = new StateMachine<BaseEnemyState>();
            var patrolMovement = new MoveToTargetComponent(enemyTransform, _configuration.PatrolMovementConfiguration);
            var chaseMovement = new MoveToTargetComponent(enemyTransform, _configuration.ChaseMovementConfiguration);
            var patrol = new RandomCirclePointPatrolComponent(_configuration.PatrolRadius, patrolMovement, patrolTarget);
            var attack = new ImmediateAttackComponent(_configuration.Damage);

            enemyBehaviour.Initialize(health, stateMachine, chaseMovement, patrol, attack);
            return enemyBehaviour;
        }
    }
}