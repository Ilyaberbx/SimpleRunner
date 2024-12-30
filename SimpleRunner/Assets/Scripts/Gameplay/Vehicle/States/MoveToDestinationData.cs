using UnityEngine;

namespace Gameplay.Vehicle.States
{
    public sealed class MoveToDestinationData
    {
        public Vector3[] Waypoints { get; }
        public Transform Source { get; }
        public MovementConfiguration MovementConfiguration { get; }

        public MoveToDestinationData(Transform source, MovementConfiguration movementConfiguration, Vector3[] waypoints)
        {
            Source = source;
            MovementConfiguration = movementConfiguration;
            Waypoints = waypoints;
        }
    }
}