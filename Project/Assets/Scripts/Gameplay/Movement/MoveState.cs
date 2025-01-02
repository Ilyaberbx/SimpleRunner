using DG.Tweening;

namespace Factura.Gameplay.Movement
{
    public sealed class MoveState
    {
        private readonly IMovable _movable;
        private Tween _moveTween;

        public MoveState(IMovable movable)
        {
            _movable = movable;
        }

        public void Enter()
        {
            _moveTween = _movable.MoveTween();
        }

        public void Exit()
        {
            _moveTween?.Kill();
        }
    }
}