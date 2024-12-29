using Gameplay.Vehicle.Modules;
using UnityEngine;

namespace Gameplay.Services
{
    [CreateAssetMenu(menuName = "Configs/Modules", fileName = "ModuleServiceSettings", order = 0)]
    public sealed class ModuleServiceSettings : ScriptableObject
    {
        [SerializeField] private ModuleFactoryConfiguration _factoryConfiguration;

        public ModuleFactoryConfiguration FactoryConfiguration => _factoryConfiguration;
    }
}