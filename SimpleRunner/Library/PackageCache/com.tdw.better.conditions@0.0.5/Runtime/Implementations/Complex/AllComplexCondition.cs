using System;
using System.Collections.Generic;
using Better.Commons.Runtime.Extensions;

namespace Better.Conditions.Runtime
{
    [Serializable]
    public class AllComplexCondition : ComplexCondition
    {
        public AllComplexCondition(IEnumerable<Condition> conditions) : base(conditions)
        {
        }

        protected AllComplexCondition()
        {
        }

        public override bool Invoke()
        {
            return CurrentSet.InvokeAll();
        }
    }
}