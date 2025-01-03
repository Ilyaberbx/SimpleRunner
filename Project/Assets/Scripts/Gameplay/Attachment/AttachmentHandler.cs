using Better.Conditions.Runtime;
using UnityEngine;

namespace Factura.Gameplay.Attachment
{
    public sealed class AttachmentHandler : IAttachable
    {
        private const string AttachmentPointNullMessage = "Attachment point is null.";
        private const string SuccessfullyAttachedMessage = "{0} successfully attached.";

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
                Debug.LogError(AttachmentPointNullMessage);
                return false;
            }

            if (!CanAttach())
            {
                return false;
            }

            _source.SetParent(attachmentPoint);
            _source.localPosition = Vector3.zero;
            Debug.Log(string.Format(SuccessfullyAttachedMessage, _source.name));
            return true;
        }
    }
}