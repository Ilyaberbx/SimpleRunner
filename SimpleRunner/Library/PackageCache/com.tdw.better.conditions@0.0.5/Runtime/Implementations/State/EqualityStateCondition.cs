using System;
using System.Collections.Generic;

namespace Better.Conditions.Runtime
{
    [Serializable]
    public class EqualityStateCondition<TState> : StateCondition<TState>
    {
        public TState State { get; set; }
        protected IEqualityComparer<TState> Comparer { get; private set; }

        public EqualityStateCondition(IEqualityComparer<TState> comparer, TState targetState, TState state = default)
            : base(targetState)
        {
            if (comparer == null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            Comparer = comparer;
            State = state;
        }

        public EqualityStateCondition(TState targetValue, TState state = default)
            : this(EqualityComparer<TState>.Default, targetValue, state)
        {
        }

        protected EqualityStateCondition() : this(default)
        {
        }

        public override void Rebuild()
        {
            base.Rebuild();

            if (Comparer == null)
            {
                Comparer = EqualityComparer<TState>.Default;
            }
        }

        public override bool Invoke()
        {
            if (Comparer == null)
            {
                return false;
            }

            return Comparer.Equals(State, TargetState);
        }

        protected override bool Validate(out Exception exception)
        {
            exception = null;
            return true;
        }
    }
}