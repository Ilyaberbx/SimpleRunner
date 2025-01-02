using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using Factura.Gameplay.Enemies;
using UnityEngine;

namespace Factura.Gameplay.Services.Enemies
{
    [Serializable]
    public sealed class EnemyService : PocoService<EnemyServiceSettings>
    {
        [SerializeField] private Transform _root;

        private IEnemyFactory _factory;

        protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            await base.OnInitializeAsync(cancellationToken);

            _factory = new EnemyFactory(Settings.FactoryConfiguration);
        }

        public EnemyBehaviour CreateEnemy(Vector3 at)
        {
            var enemy = _factory.Create(at, _root);
            return enemy;
        }
    }
}