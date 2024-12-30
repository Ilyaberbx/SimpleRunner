using System;
using System.Collections.Generic;
using Better.Locators.Runtime;

namespace Gameplay.Vehicle.Modules.Locator
{
    public sealed class ModulesLocatorSettings
    {
        public ILocator<Type, BaseModuleBehaviour> Source { get; }
        public IReadOnlyCollection<ModuleAttachmentConfiguration> AttachmentConfigurations { get; }

        public ModulesLocatorSettings(ILocator<Type, BaseModuleBehaviour> source,
            IReadOnlyCollection<ModuleAttachmentConfiguration> attachmentConfigurations)
        {
            Source = source;
            AttachmentConfigurations = attachmentConfigurations;
        }
    }
}