using System;

namespace Better.Conditions.Runtime
{
    [Serializable]
    public abstract class ApplicationCondition<TState> : StateCondition<TState>
    {
        protected ApplicationCondition(TState targetState) : base(targetState)
        {
        }
    }
}