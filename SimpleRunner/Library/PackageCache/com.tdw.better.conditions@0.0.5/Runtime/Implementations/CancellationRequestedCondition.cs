using System.Threading;

namespace Better.Conditions.Runtime
{
    public class CancellationRequestedCondition : SourceCondition<CancellationToken, bool>
    {
        public CancellationRequestedCondition(CancellationToken source, bool state = true) : base(source, state)
        {
        }

        public override bool Invoke()
        {
            return Source.IsCancellationRequested == TargetState;
        }
    }
}