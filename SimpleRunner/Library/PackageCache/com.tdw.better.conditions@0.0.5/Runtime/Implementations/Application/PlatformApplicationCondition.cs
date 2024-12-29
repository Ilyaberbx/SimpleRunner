using System;
using UnityEngine;

namespace Better.Conditions.Runtime
{
    [Serializable]
    public class PlatformApplicationCondition : ApplicationCondition<RuntimePlatform>
    {
        public PlatformApplicationCondition(RuntimePlatform state) : base(state)
        {
        }

        protected PlatformApplicationCondition() : this(RuntimePlatform.WindowsEditor)
        {
        }

        public override bool Invoke()
        {
            return Application.platform == TargetState;
        }
    }
}