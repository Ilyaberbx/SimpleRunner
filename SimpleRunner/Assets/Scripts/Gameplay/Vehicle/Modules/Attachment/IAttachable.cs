using Gameplay.Vehicle.Modules.Locator;
using UnityEngine;

namespace Gameplay.Vehicle.Modules.Attachment
{
    public interface IAttachable
    {
        bool Attach(IModulesLocatorReadonly locator, Transform attachmentPoint);
        void Detach();
    }
}