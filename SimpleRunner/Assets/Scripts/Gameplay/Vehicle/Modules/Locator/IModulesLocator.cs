using System;
using UnityEngine;

namespace Gameplay.Vehicle.Modules.Locator
{
    public interface IModulesLocatorReadonly
    {
        bool Has<TModule>() where TModule : BaseModuleBehaviour;
        bool Has(Type type);
        bool TryGetAttachmentPoint(Type type, out Transform point);
    }

    public interface IModulesLocator : IModulesLocatorReadonly
    {
        void Attach(BaseModuleBehaviour module);
        void Detach(BaseModuleBehaviour module);
    }
}