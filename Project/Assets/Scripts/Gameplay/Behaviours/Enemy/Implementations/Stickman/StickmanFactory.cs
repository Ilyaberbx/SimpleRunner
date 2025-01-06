using Better.StateMachine.Runtime;
using DG.Tweening;
using Factura.Gameplay.Animations;
using Factura.Gameplay.Attack;
using Factura.Gameplay.Health;
using Factura.Gameplay.LookAt;
using Factura.Gameplay.Movement.Target;
using Factura.Gameplay.Patrol;
using Factura.Gameplay.Services.Enemy;
using Factura.Gameplay.Target;
using UnityEngine;

namespace Factura.Gameplay.Enemy.Stickman
{
    public sealed class StickmanFactory
    {
        private readonly StickmanConfiguration _configuration;

        public StickmanFactory(StickmanConfiguration configuration)
        {
            _configuration = configuration;
        }

        public StickmanBehaviour Create(Vector3 at, Transform parent)
        {
            var prefab = _configuration.Prefab;
            var stickmanBehaviour = Object.Instantiate(prefab, at, Quaternion.identity, parent);
            var stickmanTransform = stickmanBehaviour.transform;
            var stickmanAnimator = stickmanBehaviour.SourceAnimator;

            var patrolLookAt = new ImmediateLookAtComponent(stickmanTransform);
            var chaseLookAt = patrolLookAt;
            var selfTarget = new StaticTargetComponent(stickmanTransform.position);
            var animator = new StickmanAnimatorComponent(stickmanAnimator);
            var health = new HealthComponent(_configuration.HealthAmount);
            var stateMachine = new StateMachine<BaseStickmanState>();
            var patrolMovement =
                new MoveToTargetComponent(stickmanTransform, _configuration.PatrolMovementConfiguration);
            var chaseMovement = new MoveToTargetComponent(stickmanTransform, _configuration.ChaseMovementConfiguration);
            var patrol = new RandomCirclePointPatrolComponent(_configuration.PatrolRadius, patrolMovement, selfTarget);
            var attack = new ImmediateAttackComponent(_configuration.Damage);

            stickmanBehaviour.Initialize(chaseLookAt,
                patrolLookAt,
                health,
                stateMachine,
                chaseMovement,
                patrol,
                attack,
                animator);

            return stickmanBehaviour;
        }
    }
}