using UnityEngine;

namespace Factura.Gameplay.Services.Waypoints
{
    public interface IWaypointsProvider
    {
        public Vector3[] GetWaypoints(Vector3 startPosition);
    }
}