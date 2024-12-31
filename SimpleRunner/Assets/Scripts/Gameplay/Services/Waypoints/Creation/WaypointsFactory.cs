using System;
using Factura.Gameplay.Extensions;
using UnityEngine;
using Random = System.Random;

namespace Factura.Gameplay.Services.Waypoints
{
    public sealed class WaypointsFactory : IWaypointsFactory, IDisposable
    {
        private const string ConfigurationNullReferenceMessage =
            "Can not create waypoints due to configuration null reference";

        private const int MaxValue = 100;
        private const int MinValue = 0;

        private WaypointsFactoryConfiguration _configuration;

        public WaypointsFactory(WaypointsFactoryConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Vector3[] CreateWaypoints(Vector3 startPosition, int levelLength)
        {
            if (_configuration == null)
            {
                Debug.LogError(ConfigurationNullReferenceMessage);
                return null;
            }

            var waypoints = new Vector3[_configuration.Resolution];
            var pointStep = (float)levelLength / _configuration.Resolution;
            var random = new Random();

            for (int i = 0; i < _configuration.Resolution; i++)
            {
                var randomValue = random.Next(MinValue, MaxValue);
                var turnChance = _configuration.TurnRate;
                var turnRange = 0;

                if (randomValue > turnChance)
                {
                    turnRange = random.Next(-_configuration.TurnRange, _configuration.TurnRange);
                }

                var waypoint = startPosition
                    .AddZ(pointStep * i)
                    .AddX(turnRange);

                waypoints[i] = waypoint;
            }

            return waypoints;
        }

        public void Dispose()
        {
            _configuration = null;
        }
    }
}