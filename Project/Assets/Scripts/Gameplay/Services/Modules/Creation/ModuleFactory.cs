using System.Linq;
using Factura.Gameplay.Modules;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Factura.Gameplay.Services.Modules
{
    public sealed class ModuleFactory : IModuleFactory
    {
        private const string ConfigurationNullMessage = "Configuration is null. Cannot create module.";
        private const string CannotFindDataFormat = "No creation data found for module of type {0}.";
        private readonly ModuleFactoryConfiguration _configuration;

        public ModuleFactory(ModuleFactoryConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TModule Create<TModule>(Vector3 at) where TModule : VehicleModuleBehaviour
        {
            if (_configuration == null)
            {
                Debug.LogError(ConfigurationNullMessage);
                return null;
            }

            var creationData = GetCreationData<TModule>();

            if (creationData == null)
            {
                var message = string.Format(CannotFindDataFormat, typeof(TModule).Name);
                Debug.LogWarning(message);
                return null;
            }

            var prefab = creationData.Prefab;
            var derivedModule = Object.Instantiate(prefab, at, Quaternion.identity, null);

            if (derivedModule is TModule concreteModule)
            {
                return concreteModule;
            }

            return null;
        }

        private ModuleCreationData GetCreationData<TModule>() where TModule : VehicleModuleBehaviour
        {
            return _configuration.CreationData.FirstOrDefault(data => data.KeyType == typeof(TModule));
        }
    }
}