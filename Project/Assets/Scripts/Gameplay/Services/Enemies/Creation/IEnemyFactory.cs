using Factura.Gameplay.Enemies;
using UnityEngine;

namespace Factura.Gameplay.Services.Enemies
{
    public interface IEnemyFactory
    {
        EnemyBehaviour Create(Vector3 at, Transform parent);
    }
}