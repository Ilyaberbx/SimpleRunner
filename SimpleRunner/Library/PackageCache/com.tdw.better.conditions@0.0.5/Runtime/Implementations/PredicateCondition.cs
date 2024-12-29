using System;

namespace Better.Conditions.Runtime
{
    public class PredicateCondition : Condition
    {
        private readonly Action _rebuildAction;
        private readonly Func<bool> _predicate;

        public PredicateCondition(Func<bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            _predicate = predicate;
        }

        public PredicateCondition(Action rebuildAction, Func<bool> predicate) : this(predicate)
        {
            if (rebuildAction == null)
            {
                throw new ArgumentNullException(nameof(rebuildAction));
            }

            _rebuildAction = rebuildAction;
        }

        public override void Rebuild()
        {
            base.Rebuild();

            _rebuildAction?.Invoke();
        }

        public override bool Invoke()
        {
            return _predicate.Invoke();
        }

        protected override bool Validate(out Exception exception)
        {
            if (_predicate == null)
            {
                exception = new NullReferenceException(nameof(_predicate));
                return false;
            }

            exception = null;
            return true;
        }
    }
}