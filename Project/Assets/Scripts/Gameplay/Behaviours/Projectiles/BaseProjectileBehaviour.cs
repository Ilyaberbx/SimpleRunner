using UnityEngine;

namespace Factura.Gameplay.Projectiles
{
    public abstract class BaseProjectileBehaviour : MonoBehaviour
    {
        public abstract void Initialize(Vector3 direction);
    }
}