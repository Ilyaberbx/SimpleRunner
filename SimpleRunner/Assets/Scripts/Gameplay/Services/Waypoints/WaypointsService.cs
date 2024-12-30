using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Better.Services.Runtime;
using Gameplay.Services.Level;
using UnityEngine;

namespace Gameplay.Services.Waypoints
{
    [Serializable]
    public sealed class WaypointsService : PocoService<WaypointsServiceSettings>
    {
        private LevelService _levelService;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _levelService = ServiceLocator.Get<LevelService>();
            return Task.CompletedTask;
        }

        public Vector3[] GetWaypoints(Vector3 startPosition)
        {
            using var factory = new WaypointsFactory(Settings.FactoryConfiguration);
            var levelLength = _levelService.LevelLength;
            return factory.CreateWaypoints(startPosition, levelLength);
        }
    }
}