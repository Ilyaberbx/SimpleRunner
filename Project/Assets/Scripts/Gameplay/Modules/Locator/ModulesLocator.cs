using System;
using System.Collections.Generic;
using System.Linq;
using Better.Locators.Runtime;
using UnityEngine;

namespace Factura.Gameplay.Modules.Locator
{
    public sealed class ModulesLocator : IModulesLocator
    {
        private const string ModuleIsNullMessage = "Module is null";
        private const string ModuleAlreadyAttachedFormat = "Module {0} already attached";

        private readonly ILocator<Type, VehicleModuleBehaviour> _source;
        private readonly IReadOnlyCollection<LocatorAttachmentConfiguration> _attachmentConfigurations;

        public ModulesLocator(ILocator<Type, VehicleModuleBehaviour> source,
            IReadOnlyCollection<LocatorAttachmentConfiguration> attachmentConfigurations)
        {
            _source = source;
            _attachmentConfigurations = attachmentConfigurations;
        }

        public bool Has<TModule>() where TModule : VehicleModuleBehaviour
        {
            return Has(typeof(TModule));
        }

        public bool Has(Type type)
        {
            return type != null && _source.ContainsKey(type);
        }

        public bool TryGetAttachmentPoint(Type type, out Transform point)
        {
            if (_attachmentConfigurations == null)
            {
                point = null;
                return false;
            }

            var data = _attachmentConfigurations.FirstOrDefault(temp => temp.ModuleType == type);
            if (data == null)
            {
                point = null;
                return false;
            }

            point = data.Point;
            return true;
        }

        public void Attach(VehicleModuleBehaviour module)
        {
            if (module == null)
            {
                Debug.LogError(ModuleIsNullMessage);
                return;
            }

            var moduleType = module.GetType();
            if (_source.ContainsKey(moduleType))
            {
                var message = string.Format(ModuleAlreadyAttachedFormat, moduleType.Name);
                Debug.LogWarning(message);
                return;
            }

            module.Setup(this);
            var isAttached = module.TryAttach();
            if (isAttached)
            {
                _source.TryAdd(moduleType, module);
            }
        }
    }
}