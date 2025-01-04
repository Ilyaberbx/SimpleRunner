using System;
using System.Threading;
using System.Threading.Tasks;
using Better.StateMachine.Runtime.States;
using DG.Tweening;
using Factura.Gameplay.Extensions;

namespace Factura.Gameplay.Movement
{
    public sealed class MoveState : BaseState
    {
        public event Action OnDestinationReached;
        private readonly IMovable _movable;
        private Tween _moveTween;

        public MoveState(IMovable movable)
        {
            _movable = movable;
        }

        public override Task EnterAsync(CancellationToken token)
        {
            _moveTween = _movable.MoveTween();
            return _moveTween.AsTask(token);
        }

        public override Task ExitAsync(CancellationToken token)
        {
            _moveTween?.Kill();
            return Task.CompletedTask;
        }

        public override void OnEntered()
        {
            OnDestinationReached?.Invoke();
        }

        public override void OnExited()
        {
            _moveTween = null;
        }
    }
}