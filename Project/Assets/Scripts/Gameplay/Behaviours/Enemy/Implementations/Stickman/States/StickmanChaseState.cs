using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using DG.Tweening;
using Factura.Gameplay.Animations;
using Factura.Gameplay.LookAt;
using Factura.Gameplay.Movement;
using Factura.Gameplay.Services.Update;
using Factura.Gameplay.Target;
using UnityEngine;

namespace Factura.Gameplay.Enemy.Stickman
{
    public sealed class StickmanChaseState : BaseStickmanState, IGameUpdatable
    {
        private readonly ILookAt _lookAt;
        private readonly IStickmanAnimator _animator;
        private readonly ITarget _target;
        private readonly IDynamicMovable _movement;
        private Tween _moveTween;
        private GameUpdateService _gameUpdateService;
        private CancellationToken _token;

        public StickmanChaseState(ILookAt lookAt,
            IStickmanAnimator animator,
            ITarget target,
            IDynamicMovable movement)
        {
            _lookAt = lookAt;
            _animator = animator;
            _target = target;
            _movement = movement;
        }

        public override Task EnterAsync(CancellationToken token)
        {
            _token = token;
            _gameUpdateService = ServiceLocator.Get<GameUpdateService>();
            _animator.PlayChase(true);
            _movement.SetTarget(_target);
            _moveTween = _movement.MoveTween();
            _gameUpdateService.Subscribe(this);
            return Task.CompletedTask;
        }

        public override Task ExitAsync(CancellationToken token)
        {
            _gameUpdateService.Unsubscribe(this);
            _animator.PlayChase(false);
            _moveTween?.Kill();
            return Task.CompletedTask;
        }

        public void OnUpdate(float deltaTime)
        {
            LookAt(_target.Position);
        }

        private void LookAt(Vector3 target)
        {
            _lookAt.LookAt(target, _token).Forget();
        }
    }
}