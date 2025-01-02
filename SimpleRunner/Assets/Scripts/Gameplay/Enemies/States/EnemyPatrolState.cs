using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using DG.Tweening;
using Factura.Gameplay.Extensions;
using Factura.Gameplay.Movement;
using Factura.Gameplay.Target;
using UnityEngine;

namespace Factura.Gameplay.Enemies.States
{
    public sealed class EnemyPatrolState : BaseEnemyState
    {
        private readonly Transform _source;
        private readonly float _patrolRange;
        private readonly IDynamicMovable _dynamicMovable;

        private Vector3 _leftPosition;
        private Vector3 _rightPosition;
        private Tween _moveTween;
        private bool _movingToRight;
        private CancellationTokenSource _tokenSource;

        public EnemyPatrolState(Transform source, float patrolRange, IDynamicMovable dynamicMovable)
        {
            _source = source;
            _patrolRange = patrolRange;
            _dynamicMovable = dynamicMovable;
            _movingToRight = true;
        }

        protected override void Enter()
        {
            _tokenSource = new CancellationTokenSource();
            var startPoint = _source.position;
            _leftPosition = startPoint.AddX(-_patrolRange);
            _rightPosition = startPoint.AddX(_patrolRange);
            MovePatrolCycle(_tokenSource.Token).Forget();
        }

        protected override void Exit()
        {
            _moveTween?.Kill();
            _tokenSource?.Cancel();
            _moveTween = null;
            _tokenSource = null;
        }

        private async Task MovePatrolCycle(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var targetPoint = _movingToRight ? _rightPosition : _leftPosition;

                _dynamicMovable.SetTarget(new StaticTargetHandler(targetPoint));
                _moveTween = _dynamicMovable.MoveTween();

                try
                {
                    await _moveTween.AsTask(token);
                }
                catch (OperationCanceledException)
                {
                    return;
                }

                _movingToRight = !_movingToRight;
            }
        }
    }
}