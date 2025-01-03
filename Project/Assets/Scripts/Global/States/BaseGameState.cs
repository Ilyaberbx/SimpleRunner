using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Better.StateMachine.Runtime.States;
using Factura.Global.Services;

namespace Factura.Global.States
{
    public abstract class BaseGameState : BaseState
    {
        protected GameStatesService GameStatesService { get; private set; }

        public override Task EnterAsync(CancellationToken token)
        {
            GameStatesService = ServiceLocator.Get<GameStatesService>();
            return Task.CompletedTask;
        }

        public sealed override void OnEntered()
        {
        }

        public sealed override void OnExited()
        {
        }
    }
}