using Factura.Gameplay.ModulesLocator;
using UnityEngine;

namespace Factura.Gameplay
{
    public abstract class VehicleModuleBehaviour : MonoBehaviour
    {
        private VehicleModuleType _moduleType;
        private bool IsInitialized => Locator != null && _moduleType != VehicleModuleType.None;
        protected bool IsAttached { get; private set; }
        protected IVehicleModulesLocatorReadonly Locator { get; private set; }
        public VehicleModuleType GetModuleType() => _moduleType;

        public void Initialize(VehicleModuleType type)
        {
            _moduleType = type;
        }

        public bool TryAttach(IVehicleModulesLocatorReadonly locator)
        {
            Locator = locator;
            if (!IsInitialized)
            {
                return false;
            }

            var canAttach = Locator.TryGetAttachmentPoint(_moduleType, out var attachmentPoint);
            IsAttached = canAttach && TryAttachInternal(attachmentPoint);
            return IsAttached;
        }

        protected abstract bool TryAttachInternal(Transform attachmentPoint);
    }
}