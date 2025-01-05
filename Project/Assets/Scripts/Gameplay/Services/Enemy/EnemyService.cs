using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Better.Services.Runtime;
using Factura.Gameplay.Enemy;
using Factura.Global.Services.StaticData;
using UnityEngine;

namespace Factura.Gameplay.Services.Enemy
{
    [Serializable]
    public sealed class EnemyService : PocoService
    {
        [SerializeField] private Transform _root;

        private IEnemyFactory _factory;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            var staticDataProvider = ServiceLocator.Get<GameplayStaticDataService>();
            _factory = new EnemyFactory(staticDataProvider.GetEnemyConfiguration());
            return Task.CompletedTask;
        }

        public EnemyBehaviour CreateEnemy(Vector3 at)
        {
            var enemy = _factory.Create(at, _root);
            return enemy;
        }
    }
}