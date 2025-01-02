using UnityEngine;

namespace Factura.Gameplay.Services.Modules
{
    [CreateAssetMenu(menuName = "Configs/Services/Modules", fileName = "ModuleServiceSettings", order = 0)]
    public sealed class ModuleServiceSettings : ScriptableObject
    {
        [SerializeField] private ModuleFactoryConfiguration _factoryConfiguration;

        public ModuleFactoryConfiguration FactoryConfiguration => _factoryConfiguration;
    }
}