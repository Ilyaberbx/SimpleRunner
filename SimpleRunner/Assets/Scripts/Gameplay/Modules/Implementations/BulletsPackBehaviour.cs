using Better.Conditions.Runtime;
using Factura.Gameplay.Attachment;
using Factura.Gameplay.Modules.Locator;
using UnityEngine;

namespace Factura.Gameplay.Modules
{
    public sealed class BulletsPackBehaviour : BaseModuleBehaviour
    {
        private IAttachable _attachable;

        public override void Setup(IModulesLocatorReadonly locator)
        {
            base.Setup(locator);

            _attachable = new AttachmentHandler(transform, new ActiveSelfCondition(gameObject, true));
        }

        protected override bool TryAttachInternal(Transform attachmentPoint)
        {
            return _attachable.TryAttach(attachmentPoint);
        }
    }
}