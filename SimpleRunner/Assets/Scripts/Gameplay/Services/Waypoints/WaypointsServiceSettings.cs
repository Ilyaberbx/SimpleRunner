using UnityEngine;

namespace Factura.Gameplay.Services.Waypoints
{
    [CreateAssetMenu(menuName = "Configs/Waypoints", fileName = "WaypointsServiceSettings", order = 0)]
    public class WaypointsServiceSettings : ScriptableObject
    {
        [SerializeField] private WaypointsFactoryConfiguration _factoryConfiguration;

        public WaypointsFactoryConfiguration FactoryConfiguration => _factoryConfiguration;
    }
}