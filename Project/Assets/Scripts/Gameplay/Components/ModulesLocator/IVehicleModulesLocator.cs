using System.Collections.Generic;
using UnityEngine;

namespace Factura.Gameplay.ModulesLocator
{
    public interface IVehicleModulesLocatorReadonly
    {
        IReadOnlyList<VehicleModuleBehaviour> AttachedModules { get; }
        bool Has(VehicleModuleType type);
        bool TryGetAttachmentPoint(VehicleModuleType type, out Transform point);
    }

    public interface IVehicleModulesLocator : IVehicleModulesLocatorReadonly
    {
        void Attach(VehicleModuleBehaviour module);
        public void RegisterAttachment(VehicleModuleType type, Transform point);
        public void UnregisterAttachment(VehicleModuleType type);
    }
}