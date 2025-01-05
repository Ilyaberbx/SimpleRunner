using Factura.Gameplay.Attachment;
using UnityEngine;

namespace Factura.Gameplay.BulletsPack
{
    public sealed class BulletsPackBehaviour : VehicleModuleBehaviour
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