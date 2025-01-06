using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Factura.Gameplay.Animations;
using Factura.Gameplay.LookAt;
using Factura.Gameplay.Patrol;
using Factura.Gameplay.Target;

namespace Factura.Gameplay.Enemy.Stickman
{
    public sealed class StickmanPatrolState : BaseStickmanState
    {
        private readonly ILookAt _lookAt;
        private readonly IStickmanAnimator _animator;
        private readonly IPatrol _patrol;

        public StickmanPatrolState(ILookAt lookAt,
            IStickmanAnimator animator,
            IPatrol patrol)
        {
            _lookAt = lookAt;
            _animator = animator;
            _patrol = patrol;
        }

        public override Task EnterAsync(CancellationToken token)
        {
            _animator.PlayPatrol(true);
            _patrol.ConductAsync(token).Forget();
            _patrol.OnTargetChanged += OnPatrolTargetChanged;
            return Task.CompletedTask;
        }

        public override Task ExitAsync(CancellationToken token)
        {
            _patrol.OnTargetChanged -= OnPatrolTargetChanged;
            _patrol.Stop();
            _animator.PlayPatrol(false);
            return Task.CompletedTask;
        }

        private void OnPatrolTargetChanged(ITarget target)
        {
            if (target.Position == default)
            {
                return;
            }

            _lookAt.Process(target.Position);
        }
    }
}