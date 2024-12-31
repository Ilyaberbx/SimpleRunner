using Factura.Gameplay.Tiles;
using UnityEngine;

namespace Factura.Gameplay.Services.Tiles.Creation
{
    public sealed class TilesFactory : ITilesFactory
    {
        private const string ConfigurationNullMessage = "Can not create tile due to configuration null reference";
        private readonly TilesFactoryConfiguration _configuration;

        public TilesFactory(TilesFactoryConfiguration configuration)
        {
            _configuration = configuration;
        }

        public GroundTileBehaviour Create(Vector3 at, Quaternion rotation, Transform parent = null)
        {
            if (_configuration == null)
            {
                Debug.LogError(ConfigurationNullMessage);
                return null;
            }

            var prefab = _configuration.Prefab;
            return Object.Instantiate(prefab, at, rotation, parent);
        }
    }
}