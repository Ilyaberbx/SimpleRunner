using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Better.Services.Runtime;
using Factura.Gameplay.Services.Level;
using Factura.Global.Services.StaticData;
using UnityEngine;

namespace Factura.Gameplay.Services.Waypoints
{
    [Serializable]
    public sealed class WaypointsService : PocoService, IWaypointsProvider
    {
        private LevelService _levelService;
        private WaypointsConfiguration _waypointsConfiguration;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            var staticDataProvider = ServiceLocator.Get<GameplayStaticDataService>();
            _waypointsConfiguration = staticDataProvider.GetWaypointsConfiguration();
            _levelService = ServiceLocator.Get<LevelService>();
            return Task.CompletedTask;
        }

        public Vector3[] GetWaypoints(Vector3 startPosition)
        {
            using var factory = new WaypointsFactory(_waypointsConfiguration);
            var levelLength = _levelService.LevelLength;
            return factory.CreateWaypoints(startPosition, levelLength);
        }
    }
}