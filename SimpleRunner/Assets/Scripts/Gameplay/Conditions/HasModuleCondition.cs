using System;
using Better.Conditions.Runtime;
using Factura.Gameplay.Modules.Locator;

namespace Factura.Gameplay.Conditions
{
    public sealed class HasModuleCondition : Condition
    {
        private readonly Type _moduleType;
        private readonly IModulesLocatorReadonly _locator;

        public HasModuleCondition(Type moduleType, IModulesLocatorReadonly locator)
        {
            _moduleType = moduleType;
            _locator = locator;
        }

        public override bool Invoke()
        {
            return _locator.Has(_moduleType);
        }

        protected override bool Validate(out Exception exception)
        {
            if (_locator == null)
            {
                exception = new NullReferenceException(nameof(_locator));
                return false;
            }

            if (_moduleType == null)
            {
                exception = new NullReferenceException(nameof(_moduleType));
                return false;
            }

            exception = null;
            return true;
        }
    }
}