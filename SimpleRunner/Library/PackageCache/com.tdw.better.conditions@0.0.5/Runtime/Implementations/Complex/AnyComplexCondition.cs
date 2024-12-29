using System;
using System.Collections.Generic;
using Better.Commons.Runtime.Extensions;

namespace Better.Conditions.Runtime
{
    [Serializable]
    public class AnyComplexCondition : ComplexCondition
    {
        public AnyComplexCondition(IEnumerable<Condition> conditions) : base(conditions)
        {
        }

        protected AnyComplexCondition()
        {
        }

        public override bool Invoke()
        {
            return CurrentSet.InvokeAny();
        }
    }
}