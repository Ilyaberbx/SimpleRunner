using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using Factura.Gameplay.Services.Tiles.Creation;
using Factura.Gameplay.Tiles;
using UnityEngine;

namespace Factura.Gameplay.Services.Tiles
{
    [Serializable]
    public sealed class TileService : PocoService<TilesServiceSettings>
    {
        public event Action<GroundTileBehaviour, int> OnTileCreate;

        [SerializeField] private Transform _root;

        private ITilesFactory _factory;
        private List<GroundTileBehaviour> _tiles = new();

        private float? _cachedTileLength;
        private Bounds? _cachedTileBounds;

        public float TileLength => _cachedTileLength ??= CalculateTileSize();
        public Bounds TileBounds => _cachedTileBounds ??= CalculateTileBounds();

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            _factory = new TilesFactory(Settings.FactoryConfiguration);
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public GroundTileBehaviour CreateTile(Vector3 at)
        {
            var tile = _factory.Create(at, Quaternion.identity, _root);
            _tiles.Add(tile);
            
            var index = _tiles.IndexOf(tile);
            OnTileCreate?.Invoke(tile, index);
            
            return tile;
        }

        private MeshRenderer GetPrefabMeshRenderer()
        {
            var prefab = Settings.FactoryConfiguration.Prefab;
            return prefab.GetComponent<MeshRenderer>();
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