using Gameplay.Services.Tiles.Creation;
using UnityEngine;

namespace Gameplay.Services.Tiles
{
    [CreateAssetMenu(menuName = "Configs/Tiles", fileName = "TilesServiceSettings", order = 0)]
    public class TilesServiceSettings : ScriptableObject
    {
        [SerializeField] private TilesFactoryConfiguration _factoryConfiguration;

        public TilesFactoryConfiguration FactoryConfiguration => _factoryConfiguration;
    }
}