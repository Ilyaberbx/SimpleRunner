using System;
using Better.Conditions.Runtime;

namespace Factura.Gameplay.Conditions
{
    public sealed class ValueCondition : Condition
    {
        private readonly bool _value;

        public ValueCondition(bool value)
        {
            _value = value;
        }

        public override bool Invoke()
        {
            return _value;
        }

        protected override bool Validate(out Exception exception)
        {
            exception = null;
            return true;
        }
    }
}