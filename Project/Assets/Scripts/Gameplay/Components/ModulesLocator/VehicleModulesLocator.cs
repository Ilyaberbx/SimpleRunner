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

        private readonly ILocator<VehicleModuleType, VehicleModuleBehaviour> _source;
        private readonly List<AttachmentData> _attachments;

        public VehicleModulesLocator(ILocator<VehicleModuleType, VehicleModuleBehaviour> source)
        {
            _source = source;
            _attachments = new List<AttachmentData>();
        }

        public bool Has(VehicleModuleType type)
        {
            return _source.ContainsKey(type);
        }

        public bool TryGetAttachmentPoint(VehicleModuleType moduleType, out Transform point)
        {
            if (_attachments == null)
            {
                point = null;
                return false;
            }

            var attachment = _attachments.FirstOrDefault(temp => temp.ModuleType == moduleType);
            if (attachment == null)
            {
                point = null;
                return false;
            }

            point = attachment.Point;
            return true;
        }

        public void Attach(VehicleModuleBehaviour module)
        {
            if (module == null)
            {
                Debug.LogError(ModuleIsNullMessage);
                return;
            }

            var moduleType = module.GetModuleType();
            if (_source.ContainsKey(moduleType))
            {
                var message = string.Format(ModuleAlreadyAttachedFormat, moduleType);
                Debug.LogWarning(message);
                return;
            }

            var isAttached = module.TryAttach(this);
            if (isAttached)
            {
                _source.TryAdd(moduleType, module);
            }
        }

        public void RegisterAttachment(VehicleModuleType type, Transform point)
        {
            if (_attachments.Any(temp => temp.ModuleType == type))
            {
                return;
            }

            var attachment = new AttachmentData(type, point);
            _attachments.Add(attachment);
        }

        public void UnregisterAttachment(VehicleModuleType type)
        {
            var attachment = _attachments.FirstOrDefault(temp => temp.ModuleType == type);

            if (attachment == null)
            {
                return;
            }

            _attachments.Remove(attachment);
        }
    }
}