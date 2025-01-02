using System;
using Factura.Gameplay.Movement;
using Factura.Gameplay.Target;
using UnityEngine;

namespace Factura.Gameplay.Enemies.States
{
    public sealed class EnemyChaseState : BaseEnemyState
    {
        private const float DistanceToFireComplete = 2f;
        public event Action OnChaseComplete;

        private readonly IDynamicMovable _dynamicMovable;
        private readonly Transform _source;
        private readonly ITarget _target;
        private readonly MoveState _moveState;

        public EnemyChaseState(Transform source, IDynamicMovable dynamicMovable, ITarget target)
        {
            _source = source;
            _target = target;
            _dynamicMovable = dynamicMovable;
            _moveState = new MoveState(dynamicMovable);
        }

        protected override void Enter()
        {
            _dynamicMovable.SetTarget(_target);
            _moveState.Enter();
        }

        public void Update()
        {
            if (GetDistanceToTarget() <= DistanceToFireComplete)
            {
                OnChaseComplete?.Invoke();
            }
        }

        private float GetDistanceToTarget()
        {
            return Vector3.Distance(_target.Position, _source.position);
        }

        protected override void Exit()
        {
            _moveState.Exit();
        }
    }
}