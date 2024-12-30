using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Gameplay.Extensions;
using UnityEngine;

namespace Gameplay.Vehicle.States
{
    public sealed class MoveToDestinationState : BaseVehicleState
    {
        private readonly MoveToDestinationData _data;
        private Task _movingTask;
        private TweenerCore<Quaternion, Quaternion, NoOptions> _rotationTween;

        private Transform Source => _data.Source;
        private Vector3[] Waypoints => _data.Waypoints;
        private MovementConfiguration MovementConfiguration => _data.MovementConfiguration;

        public MoveToDestinationState(MoveToDestinationData data)
        {
            _data = data;
        }

        public override async Task EnterAsync(CancellationToken token)
        {
            if (Waypoints.IsNullOrEmpty())
            {
                return;
            }

            var tween = BuildMoveTween();
            await tween.AsTask(token);
        }

        public override Task ExitAsync(CancellationToken token)
        {
            return Task.CompletedTask;
        }

        private Tween BuildMoveTween()
        {
            var speed = MovementConfiguration.MoveSpeed;
            var pathType = MovementConfiguration.PathType;
            var pathMode = MovementConfiguration.PathMode;
            var resolution = MovementConfiguration.Resolution;

            return Source.DOPath(Waypoints, speed, pathType, pathMode, resolution)
                .SetSpeedBased()
                .SetLookAt(0.01f);
        }
    }
}