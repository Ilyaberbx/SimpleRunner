using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Better.Services.Runtime;
using Factura.Global.Services.StaticData;

namespace Factura.Gameplay.Services.Level
{
    [Serializable]
    public sealed class LevelService : PocoService
    {
        public event Action OnLevelStart;
        public event Action OnLevelPreStart;
        public event Action OnLevelFinish;
        public event Action OnLevelLose;
        public event Action OnLevelWin;

        private bool _isLevelFinished;
        private IGameplayStaticDataProvider _gameplayStaticDataProvider;
        private LevelConfiguration _levelConfiguration;
        public int LevelLength => _levelConfiguration.Length;
        public bool IsLevelStarted { get; private set; }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _gameplayStaticDataProvider = ServiceLocator.Get<GameplayStaticDataService>();
            _levelConfiguration = _gameplayStaticDataProvider.GetLevelConfiguration();
            return Task.CompletedTask;
        }

        public void FireLevelStart()
        {
            OnLevelStart?.Invoke();
            IsLevelStarted = true;
        }

        public void FireLevelPreStart()
        {
            OnLevelPreStart?.Invoke();
        }

        private void FireLevelFinish()
        {
            _isLevelFinished = true;
            OnLevelFinish?.Invoke();
        }

        public void FireLevelWin()
        {
            if (_isLevelFinished)
            {
                return;
            }

            FireLevelFinish();
            OnLevelWin?.Invoke();
        }

        public void FireLevelLose()
        {
            if (_isLevelFinished)
            {
                return;
            }

            FireLevelFinish();
            OnLevelLose?.Invoke();
        }
    }
}