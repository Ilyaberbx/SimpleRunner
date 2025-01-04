using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;

namespace Factura.Gameplay.Services.Level
{
    [Serializable]
    public sealed class LevelService : PocoService<LevelServiceSettings>
    {
        public event Action OnLevelStart;
        public event Action OnLevelPreStart;
        public event Action OnLevelFinish;
        public event Action OnLevelLose;
        public event Action OnLevelWin;

        public int LevelLength => Settings.LevelLength;
        private bool _isLevelFinished;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void FireLevelStart()
        {
            OnLevelStart?.Invoke();
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