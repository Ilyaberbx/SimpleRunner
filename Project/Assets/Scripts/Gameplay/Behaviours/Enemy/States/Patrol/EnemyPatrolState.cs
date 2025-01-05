using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using DG.Tweening;
using Factura.Gameplay.Extensions;
using Factura.Gameplay.Movement;
using Factura.Gameplay.Target;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Factura.Gameplay.Enemy.States
{
    public sealed class EnemyPatrolState : BaseEnemyState
    {
        private readonly EnemyPatrolData _patrolData;
        private readonly Vector3 _startPosition;
        private Tween _moveTween;
        private CancellationTokenSource _tokenSource;
        private Transform Source => _patrolData.Source;
        private float PatrolRadius => _patrolData.PatrolRadius;
        private IDynamicMovable DynamicMovable => _patrolData.DynamicMovable;

        public EnemyPatrolState(EnemyPatrolData patrolData)
        {
            _patrolData = patrolData;
            _startPosition = Source.transform.position;
        }

        protected override void Enter()
        {
            _tokenSource = new CancellationTokenSource();
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
                var randomPosition = Random.insideUnitSphere * PatrolRadius;
                var targetPosition = randomPosition.Flat() + _startPosition;

                DynamicMovable.SetTarget(new StaticTargetComponent(targetPosition));
                _moveTween = DynamicMovable.MoveTween();

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
    }
}