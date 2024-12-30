using UnityEngine;

namespace Gameplay.Waypoints
{
    public sealed class WaypointsFactoryData
    {
        public Vector3 StartPosition { get; }
        public int Resolution { get; }
        public int TurnRange { get; }
        public int TurnChance { get; }
        public int LevelLength { get; }

        public WaypointsFactoryData(Vector3 startPosition, int resolution, int turnRange, int turnChance,
            int levelLength)
        {
            StartPosition = startPosition;
            Resolution = resolution;
            TurnRange = turnRange;
            TurnChance = turnChance;
            LevelLength = levelLength;
        }
    }
}