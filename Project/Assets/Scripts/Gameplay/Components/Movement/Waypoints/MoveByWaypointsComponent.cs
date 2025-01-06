using DG.Tweening;
using UnityEngine;

namespace Factura.Gameplay.Movement.Waypoints
{
    public sealed class MoveByWaypointsComponent : IMovable
    {
        private readonly Transform _source;
        private readonly MoveByWaypointsConfiguration _configuration;
        private readonly Vector3[] _waypoints;

        public MoveByWaypointsComponent(Transform source,
            Vector3[] waypoints,
            MoveByWaypointsConfiguration configuration)
        {
            _source = source;
            _configuration = configuration;
            _waypoints = waypoints;
        }

        public Tween MoveTween()
        {
            var speed = _configuration.MoveSpeed;
            var pathType = _configuration.PathType;
            var pathMode = _configuration.PathMode;
            var resolution = _configuration.Resolution;
            var lookAt = _configuration.LookAt;
            var curve = _configuration.Curve;

            return _source.DOPath(_waypoints, speed, pathType, pathMode, resolution)
                .SetSpeedBased()
                .SetEase(curve)
                .SetLookAt(lookAt)
                .SetId(_source);
        }
    }
}