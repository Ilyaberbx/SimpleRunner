using System;
using System.ComponentModel;
using Better.Conditions.Runtime;
using Factura.Gameplay.ModulesLocator;

namespace Factura.Gameplay.Conditions
{
    public sealed class HasModuleCondition : Condition
    {
        private readonly VehicleModuleType _moduleType;
        private readonly IVehicleModulesLocatorReadonly _locator;

        public HasModuleCondition(VehicleModuleType moduleType, IVehicleModulesLocatorReadonly locator)
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

            if (_moduleType == VehicleModuleType.None)
            {
                exception = new InvalidEnumArgumentException(nameof(_moduleType));
                return false;
            }

            exception = null;
            return true;
        }
    }
}