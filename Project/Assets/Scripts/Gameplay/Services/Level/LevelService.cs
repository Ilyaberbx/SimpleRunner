using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using UnityEngine;

namespace Factura.Gameplay.Services.Level
{
    public sealed class LevelService : MonoService
    {
        [SerializeField] private LevelConfiguration _levelConfiguration;

        public event Action OnLevelStart;
        public event Action OnLevelPreStart;
        public event Action OnLevelFinish;
        public event Action OnLevelLose;
        public event Action OnLevelWin;

        public int LevelLength => _levelConfiguration.LevelLength;
        public bool IsLevelFinished { get; private set; }

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
            IsLevelFinished = true;
            OnLevelFinish?.Invoke();
        }

        public void FireLevelWin()
        {
            if (IsLevelFinished)
            {
                return;
            }

            FireLevelFinish();
            OnLevelWin?.Invoke();
        }

        public void FireLevelLose()
        {
            if (IsLevelFinished)
            {
                return;
            }

            FireLevelFinish();
            OnLevelLose?.Invoke();
        }
    }
}