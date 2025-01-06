using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Factura.Gameplay.Launcher;
using Factura.Gameplay.Services.Update;
using Factura.Global.Services.Input;

namespace Factura.Gameplay.States
{
    public sealed class TurretActiveState : BaseTurretState, IGameUpdatable
    {
        private GameUpdateService _gameUpdateService;
        private InputService _inputService;

        private readonly ILauncher _launcher;
        private CancellationToken _token;

        public TurretActiveState(ILauncher launcher)
        {
            _launcher = launcher;
        }

        public override Task EnterAsync(CancellationToken token)
        {
            _token = token;
            _gameUpdateService = ServiceLocator.Get<GameUpdateService>();
            _inputService = ServiceLocator.Get<InputService>();

            _gameUpdateService.Subscribe(this);
            return Task.CompletedTask;
        }

        public override Task ExitAsync(CancellationToken token)
        {
            _gameUpdateService.Unsubscribe(this);
            return Task.CompletedTask;
        }

        public void OnUpdate(float deltaTime)
        {
            if (!_inputService.IsMouse(0))
            {
                return;
            }

            _launcher
                .Launch(deltaTime, _inputService.GetMousePosition(), _token)
                .Forget();
        }
    }
}