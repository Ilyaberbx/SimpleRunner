using UnityEngine;

namespace Gameplay.Waypoints
{
    public interface IWaypointsFactory
    {
        Vector3[] CreateWaypoints();
    }
}