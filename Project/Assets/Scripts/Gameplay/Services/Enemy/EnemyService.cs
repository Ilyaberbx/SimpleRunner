using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Better.Services.Runtime;
using DG.Tweening;
using Factura.Gameplay.Enemy;
using Factura.Gameplay.Enemy.Stickman;
using Factura.Global.Services.StaticData;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Factura.Gameplay.Services.Enemy
{
    [Serializable]
    public sealed class EnemyService : PocoService
    {
        [SerializeField] private Transform _root;

        private StickmanFactory _factory;
        private IGameplayStaticDataProvider _staticDataProvider;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _staticDataProvider = ServiceLocator.Get<GameplayStaticDataService>();
            _factory = new StickmanFactory(_staticDataProvider.GetStickmanConfiguration());
            return Task.CompletedTask;
        }

        public StickmanBehaviour CreateStickman(Vector3 at)
        {
            return _factory.Create(at, _root);
        }

        public void Release(BaseEnemyBehaviour enemyBehaviour)
        {
            DOTween.Kill(enemyBehaviour.transform);
            Object.Destroy(enemyBehaviour.gameObject);
        }
    }
}