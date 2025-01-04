using Better.Conditions.Runtime;
using UnityEngine;

namespace Factura.Gameplay.Attachment
{
    public sealed class AttachmentHandler : IAttachable
    {
        private readonly Transform _source;
        private readonly Condition _condition;

        public AttachmentHandler(Transform source, Condition condition)
        {
            _source = source;
            _condition = condition;
        }

        private bool CanAttach()
        {
            return _condition != null && _condition.SafeInvoke();
        }

        public bool TryAttach(Transform attachmentPoint)
        {
            if (attachmentPoint == null)
            {
                return false;
            }

            if (!CanAttach())
            {
                return false;
            }

            _source.SetParent(attachmentPoint);
            _source.localPosition = Vector3.zero;
            return true;
        }
    }
}