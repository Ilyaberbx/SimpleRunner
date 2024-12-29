using System;
using UnityEngine;

namespace Better.Conditions.Runtime
{
    [Serializable]
    public class FocusedApplicationCondition : ApplicationCondition<bool>
    {
        public FocusedApplicationCondition(bool state) : base(state)
        {
        }

        public FocusedApplicationCondition() : this(true)
        {
        }

        public override bool Invoke()
        {
            return Application.isFocused == TargetState;
        }
    }
}