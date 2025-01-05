using Better.StateMachine.Runtime;
using Factura.Gameplay.Attack;
using Factura.Gameplay.Enemy.States;
using Factura.Gameplay.Health;
using Factura.Gameplay.Movement.Target;
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

            var health = new HealthComponent(_configuration.HealthAmount);
            var stateMachine = new StateMachine<BaseEnemyState>();
            var movement = new MoveToTargetComponent(enemyTransform, _configuration.ToTargetConfiguration);
            var attack = new DamageAttack(_configuration.Damage);

            enemyBehaviour.Initialize(health, stateMachine, movement, attack);
            return enemyBehaviour;
        }
    }
}