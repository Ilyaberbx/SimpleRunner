using System.Threading;
using System.Threading.Tasks;
using Factura.Gameplay.Animations;
using Factura.Gameplay.Attack;
using Factura.Gameplay.Health;
using Factura.Gameplay.Visitors;

namespace Factura.Gameplay.Enemy.Stickman
{
    public class StickmanAttackState : BaseStickmanState
    {
        private readonly StickmanBehaviour _context;
        private readonly StickmanAnimationEventsObserver _animationEventsObserver;
        private readonly IEnemyVisitor _visitor;
        private readonly IStickmanAnimator _stickmanAnimator;

        public StickmanAttackState(StickmanBehaviour context,
            StickmanAnimationEventsObserver animationEventsObserver,
            IEnemyVisitor visitor,
            IStickmanAnimator stickmanAnimator)
        {
            _context = context;
            _animationEventsObserver = animationEventsObserver;
            _visitor = visitor;
            _stickmanAnimator = stickmanAnimator;
        }

        public override Task EnterAsync(CancellationToken token)
        {
            _stickmanAnimator.PlayAttack();
            _animationEventsObserver.OnAttack += OnAttacked;
            return Task.CompletedTask;
        }

        public override Task ExitAsync(CancellationToken token)
        {
            _animationEventsObserver.OnAttack -= OnAttacked;
            return Task.CompletedTask;
        }

        private void OnAttacked()
        {
            _visitor.Visit(_context);
        }
    }
}