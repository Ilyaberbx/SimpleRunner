using System;
using System.Collections.Generic;
using Better.Locators.Runtime;

namespace Gameplay.Vehicle.Modules.Locator
{
    public sealed class ModulesLocatorSettings
    {
        public ILocator<Type, BaseModuleBehaviour> Source { get; }
        public IReadOnlyCollection<ModuleAttachmentConfiguration> AttachmentData { get; }

        public ModulesLocatorSettings(ILocator<Type, BaseModuleBehaviour> source,
            IReadOnlyCollection<ModuleAttachmentConfiguration> attachmentData)
        {
            Source = source;
            AttachmentData = attachmentData;
        }
    }
}