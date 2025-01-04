using Better.Conditions.Runtime;
using UnityEngine;

namespace Factura.Gameplay.Attachment
{
    public interface IAttachable
    {
        bool TryAttach(Transform to);
    }
    
}