using UnityEngine;

namespace Factura.Gameplay
{
    public class BaseModuleConfiguration : ScriptableObject
    {
        [SerializeField] private VehicleModuleType _type;

        public VehicleModuleType VehicleModuleType => _type;
    }
}