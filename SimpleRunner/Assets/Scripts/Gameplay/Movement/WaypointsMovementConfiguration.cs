using System;
using DG.Tweening;
using UnityEngine;

namespace Factura.Gameplay.Movement
{
    [Serializable]
    public sealed class WaypointsMovementConfiguration
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private PathType _pathType;
        [SerializeField] private PathMode _pathMode;
        [SerializeField] private int _resolution;
        [SerializeField] private float _lookAt;

        public float MoveSpeed => _moveSpeed;
        public PathType PathType => _pathType;
        public PathMode PathMode => _pathMode;
        public int Resolution => _resolution;
        public float LookAt => _lookAt;
    }
}