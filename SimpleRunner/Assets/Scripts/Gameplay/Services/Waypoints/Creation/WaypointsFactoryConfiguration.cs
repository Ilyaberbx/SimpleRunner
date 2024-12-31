using System;
using UnityEngine;

namespace Factura.Gameplay.Services.Waypoints
{
    [Serializable]
    public sealed class WaypointsFactoryConfiguration
    {
        [SerializeField] private int _resolution;
        [SerializeField] private int _turnRange;
        [SerializeField] private int _turnRate;

        public int Resolution => _resolution;
        public int TurnRange => _turnRange;
        public int TurnRate => _turnRate;
    }
}