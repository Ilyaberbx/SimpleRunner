using System;
using Gameplay.Extensions;
using UnityEngine;
using Random = System.Random;

namespace Gameplay.Waypoints
{
    public sealed class WaypointsFactory : IWaypointsFactory, IDisposable
    {
        private const string DataNullReferenceMessage = "Can not create waypoints due to data null reference";

        private WaypointsFactoryData _data;

        public WaypointsFactory(WaypointsFactoryData data)
        {
            _data = data;
        }

        public Vector3[] CreateWaypoints()
        {
            if (_data == null)
            {
                Debug.LogError(DataNullReferenceMessage);
                return null;
            }

            var startPosition = _data.StartPosition;
            var waypoints = new Vector3[_data.Resolution];
            var pointStep = (float)_data.LevelLength / _data.Resolution;
            var random = new Random();

            for (int i = 0; i < _data.Resolution; i++)
            {
                var randomValue = random.Next(0, 100);
                var turnChance = _data.TurnChance;
                var turnRange = 0;

                if (randomValue > turnChance)
                {
                    turnRange = random.Next(-_data.TurnRange, _data.TurnRange);
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
            _data = null;
        }
    }
}