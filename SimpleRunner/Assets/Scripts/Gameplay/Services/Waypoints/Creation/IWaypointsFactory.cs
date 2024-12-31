using UnityEngine;

namespace Factura.Gameplay.Services.Waypoints
{
    public interface IWaypointsFactory
    {
        Vector3[] CreateWaypoints(Vector3 startPosition, int levelLength);
    }
}