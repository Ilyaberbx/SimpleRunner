using UnityEngine;

namespace Factura.Gameplay.Shooter
{
    public interface IShooter
    {
        void Fire(Vector3 mouseWorldPosition);
    }
}