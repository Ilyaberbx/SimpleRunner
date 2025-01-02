using Factura.Gameplay.Services.Tiles.Creation;
using UnityEngine;

namespace Factura.Gameplay.Services.Tiles
{
    [CreateAssetMenu(menuName = "Configs/Tiles", fileName = "TilesServiceSettings", order = 0)]
    public class TilesServiceSettings : ScriptableObject
    {
        [SerializeField] private TilesFactoryConfiguration _factoryConfiguration;
        [SerializeField] private float _tileSize;
        
        public TilesFactoryConfiguration FactoryConfiguration => _factoryConfiguration;
        public float TileSize => _tileSize;
    }
}