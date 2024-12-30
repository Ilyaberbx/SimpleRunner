using Gameplay.Vehicle.Modules.Locator;
using UnityEngine;

namespace Gameplay.Vehicle.Modules
{
    public abstract class BaseModuleBehaviour : MonoBehaviour
    {
        protected IModulesLocatorReadonly Locator { get; private set; }

        public bool TryAttach(IModulesLocatorReadonly locator)
        {
            Locator = locator;
            if (Locator == null)
            {
                return false;
            }

            var canAttach = locator.TryGetAttachmentPoint(GetType(), out var attachmentPoint);
            return canAttach && TryAttachInternal(locator, attachmentPoint);
        }

        protected abstract bool TryAttachInternal(IModulesLocatorReadonly locator, Transform attachmentPoint);

        public abstract void Detach();
    }
}