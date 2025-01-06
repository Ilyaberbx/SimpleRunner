using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using DG.Tweening;
using Factura.Gameplay.Extensions;
using Factura.Gameplay.LookAt;
using Factura.Gameplay.Movement;
using Factura.Gameplay.Target;
using Random = UnityEngine.Random;

namespace Factura.Gameplay.Patrol
{
    public class RandomCirclePointPatrolComponent : IPatrol
    {
        public event Action<ITarget> OnTargetChanged;

        private readonly float _patrolRadius;
        private readonly ILookAt _lookAt;
        private readonly IDynamicMovable _dynamicMovable;
        private readonly ITarget _selfTarget;

        private Tween _moveTween;
        private CancellationTokenSource _patrolCancellationSource;

        public RandomCirclePointPatrolComponent(
            float patrolRadius,
            IDynamicMovable dynamicMovable,
            ITarget selfTarget)
        {
            _patrolRadius = patrolRadius;
            _dynamicMovable = dynamicMovable;
            _selfTarget = selfTarget;
        }

        public async Task ConductAsync(CancellationToken token)
        {
            _patrolCancellationSource = CancellationTokenSource.CreateLinkedTokenSource(token);
            var patrolToken = _patrolCancellationSource.Token;

            while (!_patrolCancellationSource.IsCancellationRequested)
            {
                var randomPosition = Random.insideUnitSphere * _patrolRadius;
                var targetPosition = randomPosition.Flat() + _selfTarget.Position;

                var patrolTarget = new StaticTargetComponent(targetPosition);
                _dynamicMovable.SetTarget(patrolTarget);
                OnTargetChanged?.Invoke(patrolTarget);
                _moveTween = _dynamicMovable.MoveTween();

                try
                {
                    await _moveTween.AsTask(patrolToken);
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