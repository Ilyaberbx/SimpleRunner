using Factura.Gameplay.ModulesLocator;
using UnityEngine;

namespace Factura.Gameplay
{
    public abstract class BaseModuleBehaviour : MonoBehaviour
    {
        protected IModulesLocatorReadonly Locator { get; private set; }
        private bool IsInitialized => Locator != null;
        protected bool IsAttached { get; private set; }

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
            IsAttached = canAttach && TryAttachInternal(attachmentPoint);
            return IsAttached;
        }

        protected abstract bool TryAttachInternal(Transform attachmentPoint);
    }
}