using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Better.Services.Runtime;
using Factura.Gameplay.Services.Tile.Creation;
using Factura.Gameplay.Tile;
using Factura.Global.Services.StaticData;
using UnityEngine;

namespace Factura.Gameplay.Services.Tile
{
    [Serializable]
    public sealed class TileService : PocoService
    {
        public event Action<TileBehaviour, int> OnTileCreate;

        [SerializeField] private Transform _root;

        private TileFactory _factory;
        private List<TileBehaviour> _tiles = new();
        private TileConfiguration _tileConfiguration;

        private float? _cachedTileLength;
        private Bounds? _cachedTileBounds;

        public IReadOnlyList<TileBehaviour> Tiles => _tiles;
        public float TileLength => _cachedTileLength ??= CalculateTileSize();
        public Bounds TileBounds => _cachedTileBounds ??= CalculateTileBounds();


        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            var staticDataProvider = ServiceLocator.Get<GameplayStaticDataService>();
            _tileConfiguration = staticDataProvider.GetTileConfiguration();
            return Task.CompletedTask;
        }

        public TileBehaviour CreateTile(Vector3 at)
        {
            var tile = _factory.Create(at, Quaternion.identity, _root);
            _tiles.Add(tile);

            var index = _tiles.IndexOf(tile);
            OnTileCreate?.Invoke(tile, index);

            return tile;
        }

        private MeshRenderer GetPrefabMeshRenderer()
        {
            var prefab = _tileConfiguration.Prefab;
            return prefab.MeshRenderer;
        }

        private float CalculateTileSize()
        {
            var meshRenderer = GetPrefabMeshRenderer();
            return meshRenderer.bounds.size.z;
        }

        private Bounds CalculateTileBounds()
        {
            var meshRenderer = GetPrefabMeshRenderer();
            return meshRenderer.bounds;
        }
    }
}