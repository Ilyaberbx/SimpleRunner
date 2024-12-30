using System;
using System.Linq;
using Better.Locators.Runtime;
using UnityEngine;

namespace Gameplay.Vehicle.Modules.Locator
{
    public sealed class ModulesLocator : IModulesLocator
    {
        private readonly ModulesLocatorSettings _settings;
        private const string ModuleIsNullMessage = "You are trying attach null module";
        private const string ModuleAlreadyAttachedFormat = "Module {0} already attached";

        private ILocator<Type, BaseModuleBehaviour> Source => _settings.Source;

        public ModulesLocator(ModulesLocatorSettings settings)
        {
            _settings = settings;
        }

        public bool Has<TModule>() where TModule : BaseModuleBehaviour
        {
            return Has(typeof(TModule));
        }

        public bool Has(Type type)
        {
            return type != null && Source.ContainsKey(type);
        }

        public bool TryGetAttachmentPoint(Type type, out Transform point)
        {
            if (_settings?.AttachmentData == null)
            {
                point = null;
                return false;
            }

            var data = _settings.AttachmentData.FirstOrDefault(temp => temp.ModuleType == type);
            if (data == null)
            {
                point = null;
                return false;
            }

            point = data.Point;
            return true;
        }

        public void Attach(BaseModuleBehaviour module)
        {
            if (module == null)
            {
                Debug.LogError(ModuleIsNullMessage);
                return;
            }

            var moduleType = module.GetType();
            if (Source.ContainsKey(moduleType))
            {
                var message = string.Format(ModuleAlreadyAttachedFormat, moduleType.Name);
                Debug.LogWarning(message);
                return;
            }

            var isAttached = module.TryAttach(this);
            if (isAttached)
            {
                Source.TryAdd(moduleType, module);
            }
        }

        public void Detach(BaseModuleBehaviour module)
        {
            var hasModule = Source.TryGet(module.GetType(), out var moduleToDetach);
            if (!hasModule) return;
            moduleToDetach.Detach();
            Source.Remove(moduleToDetach);
        }
    }
}