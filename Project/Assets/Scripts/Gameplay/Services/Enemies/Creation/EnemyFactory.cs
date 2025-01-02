using Factura.Gameplay.Enemies;
using UnityEngine;

namespace Factura.Gameplay.Services.Enemies
{
    public class EnemyFactory : IEnemyFactory
    {
        private const string ConfigurationNullMessage = "Can not create enemy due to configuration null reference";

        private readonly EnemyFactoryConfiguration _configuration;

        public EnemyFactory(EnemyFactoryConfiguration configuration)
        {
            _configuration = configuration;
        }

        public EnemyBehaviour Create(Vector3 at, Transform parent)
        {
            if (_configuration == null)
            {
                Debug.LogError(ConfigurationNullMessage);
                return null;
            }

            var prefab = _configuration.Prefab;
            return Object.Instantiate(prefab, at, Quaternion.identity, parent);
        }
    }
}