using Factura.Gameplay.Enemy;
using UnityEngine;

namespace Factura.Gameplay.Services.Enemy
{
    public interface IEnemyFactory
    {
        EnemyBehaviour Create(Vector3 at, Transform parent);
    }
}