using Better.Conditions.Runtime;
using Gameplay.Vehicle.Modules.Locator;
using UnityEngine;

namespace Gameplay.Vehicle.Modules.Attachment
{
    public sealed class AttachmentHandler : IAttachable
    {
        private const string AttachmentPointNullMessage = "Attachment point is null.";
        private const string LocatorNullMessage = "Modules locator is null.";
        private const string SuccessfullyAttachedMessage = "{0} successfully attached.";
        private const string SuccessfullyDetachedMessage = "{0} detached.";

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

        public bool Attach(IModulesLocatorReadonly locator, Transform attachmentPoint)
        {
            if (locator == null)
            {
                Debug.LogError(LocatorNullMessage);
                return false;
            }

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

        public void Detach()
        {
            _transform.SetParent(null);
            Debug.Log(string.Format(SuccessfullyDetachedMessage, _transform.name));
        }
    }
}