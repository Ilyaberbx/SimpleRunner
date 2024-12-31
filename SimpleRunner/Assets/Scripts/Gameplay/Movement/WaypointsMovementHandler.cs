using DG.Tweening;
using Factura.Gameplay.Vehicle.States;
using UnityEngine;

namespace Factura.Gameplay.Movement
{
    public sealed class WaypointsMovementHandler : IMovable
    {
        private readonly Transform _source;
        private readonly WaypointsMovementConfiguration _configuration;

        public WaypointsMovementHandler(Transform source, WaypointsMovementConfiguration configuration)
        {
            _source = source;
            _configuration = configuration;
        }

        public Tween MoveTween(Vector3[] waypoints)
        {
            var speed = _configuration.MoveSpeed;
            var pathType = _configuration.PathType;
            var pathMode = _configuration.PathMode;
            var resolution = _configuration.Resolution;
            var lookAt = _configuration.LookAt;

            return _source.DOPath(waypoints, speed, pathType, pathMode, resolution)
                .SetSpeedBased()
                .SetLookAt(lookAt);
        }
    }
}