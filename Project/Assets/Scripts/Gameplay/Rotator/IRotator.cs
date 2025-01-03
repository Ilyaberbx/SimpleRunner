using UnityEngine;

namespace Factura.Gameplay.Rotator
{
    public interface IRotator
    {
        void RotateTo(Vector3 mouseWorldPosition);
    }
}