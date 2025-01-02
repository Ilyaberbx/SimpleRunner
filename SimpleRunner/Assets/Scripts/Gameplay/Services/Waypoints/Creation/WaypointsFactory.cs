using System;
using Factura.Gameplay.Extensions;
using UnityEngine;
using Random = System.Random;

namespace Factura.Gameplay.Services.Waypoints
{
    public sealed class WaypointsFactory : IDisposable
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
            var resolution = _configuration.Resolution;
            var forwardStep = (float)levelLength / resolution;
            var random = new Random();
            var previousForwardStep = 0f;

            for (var i = 0; i < resolution; i++)
            {
                var turnStep = GetTurnStep(random, i);
                var actualForwardStep = previousForwardStep += forwardStep;

                var waypoint = startPosition
                    .AddZ(actualForwardStep)
                    .AddX(turnStep);
                
                waypoints[i] = waypoint;
            }

            return waypoints;
        }

        public void Dispose()
        {
            _configuration = null;
        }

        private int GetTurnStep(Random random, int i)
        {
            var turnStep = 0;

            if (CanTurn(random, i))
            {
                turnStep = random.Next(-_configuration.TurnRange, _configuration.TurnRange);
            }

            return turnStep;
        }

        private bool CanTurn(Random random, int index)
        {
            var isLastWaypoint = index == _configuration.Resolution - 1;
            var isFirstWaypoint = index == 0;
            var randomValue = random.Next(MinValue, MaxValue);
            var turnRate = _configuration.TurnRate;

            return randomValue > turnRate && !isFirstWaypoint && !isLastWaypoint;
        }
    }
}