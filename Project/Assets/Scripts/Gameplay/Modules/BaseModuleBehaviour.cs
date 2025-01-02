using Factura.Gameplay.Modules.Locator;
using UnityEngine;

namespace Factura.Gameplay.Modules
{
    public abstract class BaseModuleBehaviour : MonoBehaviour
    {
        protected IModulesLocatorReadonly Locator { get; private set; }

        private bool IsInitialized => Locator != null;

        public virtual void Setup(IModulesLocatorReadonly locator)
        {
            Locator = locator;
        }

        public bool TryAttach()
        {
            if (!IsInitialized)
            {
                return false;
            }

            var canAttach = Locator.TryGetAttachmentPoint(GetType(), out var attachmentPoint);
            return canAttach && TryAttachInternal(attachmentPoint);
        }

        protected abstract bool TryAttachInternal(Transform attachmentPoint);
    }
}