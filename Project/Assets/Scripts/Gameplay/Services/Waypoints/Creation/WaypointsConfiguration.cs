using UnityEngine;

namespace Factura.Gameplay.Services.Waypoints
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Waypoints", fileName = "WaypointsConfiguration", order = 0)]
    public sealed class WaypointsConfiguration : ScriptableObject
    {
        [SerializeField] private int _resolution;
        [SerializeField] private int _turnRange;
        [SerializeField] private int _turnRate;

        public int Resolution => _resolution;
        public int TurnRange => _turnRange;
        public int TurnRate => _turnRate;
    }
}