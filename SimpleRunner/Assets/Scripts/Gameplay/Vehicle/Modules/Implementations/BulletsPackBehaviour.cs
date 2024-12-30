using Better.Conditions.Runtime;
using Gameplay.Vehicle.Modules.Attachment;
using Gameplay.Vehicle.Modules.Locator;
using UnityEngine;

namespace Gameplay.Vehicle.Modules
{
    public sealed class BulletsPackBehaviour : BaseModuleBehaviour
    {
        private IAttachable _attachmentHandler;

        private void Awake()
        {
            _attachmentHandler = new AttachmentHandler(transform, new ActiveSelfCondition(gameObject, true));
        }

        protected override bool TryAttachInternal(IModulesLocatorReadonly locator, Transform attachmentPoint)
        {
            return _attachmentHandler.Attach(locator, attachmentPoint);
        }

        public override void Detach()
        {
            _attachmentHandler?.Detach();
        }
    }
}