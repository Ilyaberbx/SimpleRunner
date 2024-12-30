using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using UnityEngine;

namespace Gameplay.Services.Level
{
    public sealed class LevelService : MonoService
    {
        [SerializeField] private LevelConfiguration _levelConfiguration;

        public event Action OnLevelStart;
        public event Action OnLevelFinish;
        
        public int LevelLength => _levelConfiguration.LevelLength;

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

        public void FireLevelFinish()
        {
            OnLevelFinish?.Invoke();
        }
    }
}