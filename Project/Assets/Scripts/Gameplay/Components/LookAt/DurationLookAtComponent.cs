using System;
using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;
using Factura.Gameplay.Extensions;
using UnityEngine;

namespace Factura.Gameplay.LookAt
{
    public sealed class DurationLookAtComponent : ILookAt
    {
        private readonly Transform _source;
        private readonly float _lookAtSpeed;
        private readonly AxisConstraint _constraint;
        private Tweener _tween;

        public DurationLookAtComponent(Transform source, float lookAtSpeed, AxisConstraint constraint)
        {
            _source = source;
            _lookAtSpeed = lookAtSpeed;
            _constraint = constraint;
        }

        public Task LookAt(Vector3 target, CancellationToken token)
        {
            _tween = _source
                .DOLookAt(target, _lookAtSpeed, _constraint)
                .SetSpeedBased();

            return _tween.AsTask(token);
        }
    }
}