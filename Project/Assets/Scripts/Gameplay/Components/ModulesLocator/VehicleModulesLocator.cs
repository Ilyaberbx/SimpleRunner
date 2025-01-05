using System;
using System.Collections.Generic;
using System.Linq;
using Better.Locators.Runtime;
using UnityEngine;

namespace Factura.Gameplay.ModulesLocator
{
    public sealed class VehicleModulesLocator : IVehicleModulesLocator
    {
        private const string ModuleIsNullMessage = "Module is null";
        private const string ModuleAlreadyAttachedFormat = "Module {0} already attached";

        private readonly ILocator<Type, VehicleModuleBehaviour> _source;
        private readonly IReadOnlyCollection<LocatorAttachmentData> _attachmentData;

        public VehicleModulesLocator(ILocator<Type, VehicleModuleBehaviour> source,
            IReadOnlyCollection<LocatorAttachmentData> attachmentData)
        {
            _source = source;
            _attachmentData = attachmentData;
        }

        public bool Has(Type type)
        {
            return type != null && _source.ContainsKey(type);
        }

        public bool TryGetAttachmentPoint(Type type, out Transform point)
        {
            if (_attachmentData == null)
            {
                point = null;
                return false;
            }

            var data = _attachmentData.FirstOrDefault(temp => temp.ModuleType == type);
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

            module.SetupLocator(this);
            var isAttached = module.TryAttach();
            if (isAttached)
            {
                _source.TryAdd(moduleType, module);
            }
        }
    }
}