using Better.Conditions.Runtime;
using UnityEngine;

namespace Factura.Gameplay.Attachment
{
    public sealed class AttachmentHandler : IAttachable
    {
        private const string AttachmentPointNullMessage = "Attachment point is null.";
        private const string SuccessfullyAttachedMessage = "{0} successfully attached.";

        private readonly Transform _transform;
        private readonly Condition _condition;

        public AttachmentHandler(Transform transform, Condition condition)
        {
            _transform = transform;
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

            _transform.SetParent(attachmentPoint);
            _transform.localPosition = Vector3.zero;
            Debug.Log(string.Format(SuccessfullyAttachedMessage, _transform.name));
            return true;
        }
    }
}