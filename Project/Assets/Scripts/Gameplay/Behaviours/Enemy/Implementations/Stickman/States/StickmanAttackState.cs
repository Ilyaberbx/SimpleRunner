using System.Threading;
using System.Threading.Tasks;
using Factura.Gameplay.Animations;
using Factura.Gameplay.Attack;
using Factura.Gameplay.Health;

namespace Factura.Gameplay.Enemy.Stickman
{
    public class StickmanAttackState : BaseStickmanState
    {
        private readonly StickmanAnimationEventsObserver _animationEventsObserver;
        private readonly IStickmanAnimator _stickmanAnimator;
        private readonly IHealth _targetHealth;
        private readonly IAttack _attack;
        private CancellationToken _token;

        public StickmanAttackState(StickmanAnimationEventsObserver animationEventsObserver,
            IStickmanAnimator stickmanAnimator,
            IHealth targetHealth,
            IAttack attack)
        {
            _animationEventsObserver = animationEventsObserver;
            _stickmanAnimator = stickmanAnimator;
            _targetHealth = targetHealth;
            _attack = attack;
        }

        public override Task EnterAsync(CancellationToken token)
        {
            _token = token;
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
            _attack.ProcessAsync(_targetHealth, _token);
        }
    }
}