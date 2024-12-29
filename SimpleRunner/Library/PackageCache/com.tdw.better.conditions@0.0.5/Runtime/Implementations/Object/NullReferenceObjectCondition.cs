using System;
using Object = UnityEngine.Object;

namespace Better.Conditions.Runtime
{
    [Serializable]
    public class NullReferenceObjectCondition : ObjectCondition<bool>
    {
        public NullReferenceObjectCondition(Object source, bool state) : base(source, state)
        {
        }

        protected NullReferenceObjectCondition() : this(default, true)
        {
        }

        public override bool Invoke()
        {
            var isNull = Source == null;
            return isNull == TargetState;
        }

        protected override bool Validate(out Exception exception)
        {
            exception = null;
            return false;
        }
    }
}