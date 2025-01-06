using UnityEngine;

namespace Factura.Gameplay.Projectiles
{
    public abstract class BaseProjectileBehaviour : MonoBehaviour
    {
        public abstract void Fire(Vector3 direction);
    }
}