using System;
using DG.Tweening;
using UnityEngine;

namespace Gameplay.Vehicle.States
{
    [Serializable]
    public sealed class MovementConfiguration
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private PathType _pathType;
        [SerializeField] private PathMode _pathMode;
        [SerializeField] private int _resolution;
        [SerializeField] private float _rotateSpeed;
        
        public float MoveSpeed => _moveSpeed;
        public PathType PathType => _pathType;
        public PathMode PathMode => _pathMode;
        public int Resolution => _resolution;

        public float RotateSpeed => _rotateSpeed;
    }
}