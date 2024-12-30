using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using Gameplay.Services.Tiles.Creation;
using Gameplay.Tiles;
using UnityEngine;

namespace Gameplay.Services.Tiles
{
    [Serializable]
    public sealed class TilesService : PocoService<TilesServiceSettings>
    {
        [SerializeField] private Transform _root;

        private ITilesFactory _factory;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            _factory = new TilesFactory(Settings.FactoryConfiguration);
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public GroundTileBehaviour Create(Vector3 at)
        {
            var tile = _factory.Create(at, Quaternion.identity, _root);
            return tile;
        }
    }
}