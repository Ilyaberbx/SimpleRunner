using Factura.Gameplay.Attachment;
using UnityEngine;

namespace Factura.Gameplay.Modules
{
    public sealed class BulletsPackBehaviour : BaseModuleBehaviour
    {
        private IAttachable _attachment;

        public void Initialize(IAttachable attachment)
        {
            _attachment = attachment;
        }

        protected override bool TryAttachInternal(Transform attachmentPoint)
        {
            return _attachment.TryAttach(attachmentPoint);
        }
    }
}