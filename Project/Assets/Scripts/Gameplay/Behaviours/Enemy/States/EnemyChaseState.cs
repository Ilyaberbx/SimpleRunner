using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;
using Factura.Gameplay.Movement;
using Factura.Gameplay.Target;

namespace Factura.Gameplay.Enemy.States
{
    public sealed class EnemyChaseState : BaseEnemyState
    {
        private readonly ITarget _target;
        private readonly IDynamicMovable _movement;
        private Tween _moveTween;

        public EnemyChaseState(ITarget target, IDynamicMovable movement)
        {
            _target = target;
            _movement = movement;
        }

        public override Task EnterAsync(CancellationToken token)
        {
            _movement.SetTarget(_target);
            _moveTween = _movement.MoveTween();
            return Task.CompletedTask;
        }

        public override Task ExitAsync(CancellationToken token)
        {
            _moveTween?.Kill();
            return Task.CompletedTask;
        }
    }
}