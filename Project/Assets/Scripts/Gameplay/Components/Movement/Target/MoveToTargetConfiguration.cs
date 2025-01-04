using System;
using UnityEngine;

namespace Factura.Gameplay.Movement.Target
{
    [Serializable]
    public sealed class MoveToTargetConfiguration
    {
        [SerializeField] private AnimationCurve _curve;
        [SerializeField] private float _speed;

        public float Speed => _speed;

        public AnimationCurve Curve => _curve;
    }
}