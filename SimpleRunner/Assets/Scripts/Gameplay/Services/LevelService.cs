using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using Gameplay.Waypoints;
using UnityEngine;

namespace Gameplay.Services
{
    public sealed class LevelService : MonoService
    {
        [SerializeField] private LevelConfiguration _levelConfiguration;

        public event Action OnLevelStarted;

        private Vector3[] _waypoints;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Vector3[] GetWaypoints(Vector3 startPosition)
        {
            if (_waypoints != null) return _waypoints;

            var factoryData = new WaypointsFactoryData(startPosition,
                _levelConfiguration.Resolution,
                _levelConfiguration.TurnRange,
                _levelConfiguration.TurnChance,
                _levelConfiguration.LevelLength);

            using var waypointsFactory = new WaypointsFactory(factoryData);
            return _waypoints = waypointsFactory.CreateWaypoints();
        }

        public void FireStartLevel()
        {
            OnLevelStarted?.Invoke();
        }
    }
}