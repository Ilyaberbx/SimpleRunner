using System;
using UnityEngine;

namespace Factura.Gameplay.ModulesLocator
{
    public interface IVehicleModulesLocatorReadonly
    {
        bool Has(Type type);
        bool TryGetAttachmentPoint(Type type, out Transform point);
    }

    public interface IVehicleModulesLocator : IVehicleModulesLocatorReadonly
    {
        void Attach(VehicleModuleBehaviour module);
    }
}