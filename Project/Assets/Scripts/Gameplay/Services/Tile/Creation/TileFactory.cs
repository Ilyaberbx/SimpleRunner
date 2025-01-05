using Factura.Gameplay.Tile;
using UnityEngine;

namespace Factura.Gameplay.Services.Tile.Creation
{
    public sealed class TileFactory 
    {
        private const string ConfigurationNullMessage = "Can not create tile due to configuration null reference";
        private readonly TileConfiguration _configuration;

        public TileFactory(TileConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TileBehaviour Create(Vector3 at, Quaternion rotation, Transform parent = null)
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