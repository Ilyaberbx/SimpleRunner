using UnityEngine;

namespace Factura.Gameplay.ModulesLocator
{
    public sealed class AttachmentData
    {
        public VehicleModuleType ModuleType { get; }
        public Transform Point { get; }

        public AttachmentData(VehicleModuleType moduleType, Transform point)
        {
            ModuleType = moduleType;
            Point = point;
        }
    }
}