using System;
using UnityEngine;

namespace Factura.Gameplay.ModulesLocator
{
    public interface IModulesLocatorReadonly
    {
        bool Has<TModule>() where TModule : BaseModuleBehaviour;
        bool Has(Type type);
        bool TryGetAttachmentPoint(Type type, out Transform point);
    }

    public interface IVehicleModulesLocator : IModulesLocatorReadonly
    {
        void Attach(BaseModuleBehaviour module);
    }
}