using Better.Conditions.Runtime;
using UnityEngine;

namespace Factura.Gameplay.Attachment
{
    public sealed class ImmediateAttachmentComponent : IAttachable
    {
        private readonly Transform _source;
        private readonly Condition _condition;

        public ImmediateAttachmentComponent(Transform source, Condition condition)
        {
            _source = source;
            _condition = condition;
        }

        private bool CanAttach()
        {
            return _condition != null && _condition.SafeInvoke();
        }

        public bool TryAttach(Transform to)
        {
            if (to == null)
            {
                return false;
            }

            if (!CanAttach())
            {
                return false;
            }

            _source.SetParent(to);
            _source.localPosition = Vector3.zero;
            return true;
        }
    }
}