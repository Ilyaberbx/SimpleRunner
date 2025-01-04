using Factura.Gameplay.Enemy;
using UnityEngine;

namespace Factura.Gameplay.Services.Enemies
{
    public class EnemyFactory : IEnemyFactory
    {
        private const string ConfigurationNullMessage = "Can not create enemy due to configuration null reference";
        private readonly EnemyBehaviour _prefab;

        public EnemyFactory(EnemyBehaviour prefab)
        {
            _prefab = prefab;
        }

        public EnemyBehaviour Create(Vector3 at, Transform parent)
        {
            if (_prefab == null)
            {
                Debug.LogError(ConfigurationNullMessage);
                return null;
            }

            return Object.Instantiate(_prefab, at, Quaternion.identity, parent);
        }
    }
}