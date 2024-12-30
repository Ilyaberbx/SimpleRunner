using System;
using Better.Locators.Runtime;
using Gameplay.Vehicle.Modules;
using Gameplay.Vehicle.Modules.Locator;
using UnityEngine;

namespace Gameplay.Vehicle
{
    public sealed class VehicleBehaviour : MonoBehaviour
    {
        [SerializeField] private ModuleAttachmentData[] _attachmentData;
        private ModulesLocator _locator;

        private void Awake()
        {
            InitializeLocator();
        }

        private void InitializeLocator()
        {
            var source = new Locator<Type, BaseModuleBehaviour>();
            var settings = new ModulesLocatorSettings(source, _attachmentData);
            _locator = new ModulesLocator(settings);
        }

        public void Attach(BaseModuleBehaviour moduleBehaviour)
        {
            _locator.Attach(moduleBehaviour);
        }

        public void Detach(BaseModuleBehaviour moduleBehaviour)
        {
            _locator.Detach(moduleBehaviour);
        }
    }
}