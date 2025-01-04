using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Factura.Gameplay.Target;
using UnityEngine;

namespace Factura.Gameplay.Movement.Target
{
    public sealed class MoveToTargetComponent : IDynamicMovable
    {
        private readonly Transform _source;
        private readonly MoveToTargetConfiguration _configuration;
        private ITarget _target;

        private TweenerCore<Vector3, Vector3, VectorOptions> _tween;

        public MoveToTargetComponent(Transform source, MoveToTargetConfiguration configuration)
        {
            _source = source;
            _configuration = configuration;
        }

        public void SetTarget(ITarget target)
        {
            _target = target;
        }

        public Tween MoveTween()
        {
            var speed = _configuration.Speed;
            var curve = _configuration.Curve;

            _tween = _source
                .DOMove(_target.Position, speed)
                .SetSpeedBased()
                .SetEase(curve)
                .OnUpdate(OnTweenUpdated);

            return _tween;
        }

        private void OnTweenUpdated()
        {
            _tween.ChangeEndValue(_target.Position, true);
        }
    }
}