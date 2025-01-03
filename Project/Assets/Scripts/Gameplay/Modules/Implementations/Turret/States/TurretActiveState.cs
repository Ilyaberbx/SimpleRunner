using Better.Locators.Runtime;
using Factura.Gameplay.Launcher;
using Factura.Gameplay.Services.Update;
using Factura.Global.Services;

namespace Factura.Gameplay.Modules.States
{
    public sealed class TurretActiveState : BaseTurretState, IGameUpdatable
    {
        private GameUpdateService _gameUpdateService;
        private InputService _inputService;

        private readonly ILauncher _launcher;

        public TurretActiveState(ILauncher launcher)
        {
            _launcher = launcher;
        }

        protected override void Enter()
        {
            _gameUpdateService = ServiceLocator.Get<GameUpdateService>();
            _inputService = ServiceLocator.Get<InputService>();
            
            _gameUpdateService.Subscribe(this);
        }

        protected override void Exit()
        {
            _gameUpdateService.Unsubscribe(this);
        }

        public void OnUpdate(float deltaTime)
        {
            if (!_inputService.IsMouse(0))
            {
                return;
            }

            _launcher.Launch(deltaTime, _inputService.GetMousePosition());
        }
    }
}