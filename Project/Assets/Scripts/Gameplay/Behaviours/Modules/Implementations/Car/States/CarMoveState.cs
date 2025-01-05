using System;
using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;
using Factura.Gameplay.Movement;

namespace Factura.Gameplay.Car.States
{
    public sealed class CarMoveState : BaseCarState
    {
        public event Action OnDestinationReached;

        private readonly IMovable _movable;
        private Tween _moveTween;

        public CarMoveState(IMovable movable)
        {
            _movable = movable;
        }

        public override Task EnterAsync(CancellationToken token)
        {
            _moveTween = _movable.MoveTween();
            _moveTween.OnComplete(OnTweenFinished);
            return Task.CompletedTask;
        }

        public override Task ExitAsync(CancellationToken token)
        {
            _moveTween?.Kill();
            return Task.CompletedTask;
        }

        private void OnTweenFinished()
        {
            OnDestinationReached?.Invoke();
        }
    }
}