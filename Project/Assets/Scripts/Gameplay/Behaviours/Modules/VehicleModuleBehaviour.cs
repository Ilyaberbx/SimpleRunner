using Factura.Gameplay.ModulesLocator;
using UnityEngine;

namespace Factura.Gameplay
{
    public abstract class VehicleModuleBehaviour : MonoBehaviour
    {
        protected IVehicleModulesLocatorReadonly Locator { get; private set; }
        private bool IsInitialized => Locator != null;
        protected bool IsAttached { get; private set; }

        public virtual void SetupLocator(IVehicleModulesLocatorReadonly locator)
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