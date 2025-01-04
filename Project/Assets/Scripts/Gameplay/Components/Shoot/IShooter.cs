using UnityEngine;

namespace Factura.Gameplay.Shoot
{
    public interface IShooter
    {
        void Shot(Vector3 mouseWorldPosition);
    }
}