using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using DG.Tweening;
using Factura.Gameplay.Extensions;
using Factura.Gameplay.Movement;
using UnityEngine;

namespace Factura.Gameplay.Vehicle.States
{
    public sealed class MoveToDestinationState : BaseVehicleState
    {
        private readonly IMovable _movable;
        private readonly Vector3[] _waypoints;
        private Tween _moveTween;

        public MoveToDestinationState(IMovable movable, Vector3[] waypoints)
        {
            _movable = movable;
            _waypoints = waypoints;
        }

        public override async Task EnterAsync(CancellationToken token)
        {
            if (_waypoints.IsNullOrEmpty())
            {
                return;
            }

            _moveTween = _movable.MoveTween(_waypoints);
            await _moveTween.AsTask(token);
        }

        public override Task ExitAsync(CancellationToken token)
        {
            _moveTween.Kill();
            return Task.CompletedTask;
        }
    }
}