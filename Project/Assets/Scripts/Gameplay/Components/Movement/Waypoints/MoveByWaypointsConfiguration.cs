using System;
using DG.Tweening;
using UnityEngine;

namespace Factura.Gameplay.Movement.Waypoints
{
    [Serializable]
    public sealed class MoveByWaypointsConfiguration
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private AnimationCurve _curve;
        [SerializeField] private PathType _pathType;
        [SerializeField] private PathMode _pathMode;
        [SerializeField] private int _resolution;
        [SerializeField] private float _lookAt;

        public float MoveSpeed => _moveSpeed;
        public PathType PathType => _pathType;
        public PathMode PathMode => _pathMode;
        public int Resolution => _resolution;
        public float LookAt => _lookAt;
        public AnimationCurve Curve => _curve;
    }
}