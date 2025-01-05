using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using DG.Tweening;
using Factura.Gameplay.Extensions;
using Factura.Gameplay.Movement;
using Factura.Gameplay.Target;
using Random = UnityEngine.Random;

namespace Factura.Gameplay.Patrol
{
    public class RandomCirclePointPatrolComponent : IPatrol
    {
        private readonly float _patrolRadius;
        private readonly IDynamicMovable _dynamicMovable;
        private readonly ITarget _target;

        private Tween _moveTween;
        private CancellationTokenSource _patrolCancellationSource;

        public RandomCirclePointPatrolComponent(
            float patrolRadius,
            IDynamicMovable dynamicMovable,
            ITarget target)
        {
            _patrolRadius = patrolRadius;
            _dynamicMovable = dynamicMovable;
            _target = target;
        }

        public async Task ConductAsync(CancellationToken token)
        {
            _patrolCancellationSource = CancellationTokenSource.CreateLinkedTokenSource(token);

            while (!_patrolCancellationSource.IsCancellationRequested)
            {
                var randomPosition = Random.insideUnitSphere * _patrolRadius;
                var targetPosition = randomPosition.Flat() + _target.Position;

                _dynamicMovable.SetTarget(new StaticTargetComponent(targetPosition));
                _moveTween = _dynamicMovable.MoveTween();

                try
                {
                    await _moveTween.AsTask(token);
                }
                catch (OperationCanceledException)
                {
                    return;
                }
            }
        }

        public void Stop()
        {
            _patrolCancellationSource?.Cancel();
            _moveTween?.Kill();
            _moveTween = null;
            _patrolCancellationSource = null;
        }
    }
}