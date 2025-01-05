using UnityEngine;

namespace Factura.Gameplay.Attachment
{
    public sealed class ImmediateAttachmentComponent : IAttachable
    {
        private readonly Transform _source;

        public ImmediateAttachmentComponent(Transform source)
        {
            _source = source;
        }


        public bool TryAttach(Transform to)
        {
            if (to == null)
            {
                return false;
            }

            _source.SetParent(to);
            _source.localPosition = Vector3.zero;
            return true;
        }
    }
}