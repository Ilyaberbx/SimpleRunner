using System.Threading;
using System.Threading.Tasks;
using Factura.Gameplay.Animations;

namespace Factura.Gameplay.Enemy.Stickman
{
    public sealed class StickmanIdleState : BaseStickmanState
    {
        private readonly IStickmanAnimator _animator;

        public StickmanIdleState(IStickmanAnimator animator)
        {
            _animator = animator;
        }

        public override Task EnterAsync(CancellationToken token)
        {
            _animator.PlayIdle();
            return Task.CompletedTask;
        }

        public override Task ExitAsync(CancellationToken token)
        {
            return Task.CompletedTask;
        }
    }
}