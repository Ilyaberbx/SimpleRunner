using Factura.Gameplay.Tile;
using UnityEngine;

namespace Factura.Gameplay.Services.Tile.Creation
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Tile", fileName = "TileConfiguration", order = 0)]
    public sealed class TileConfiguration : ScriptableObject
    {
        [SerializeField] private TileBehaviour _prefab;

        public TileBehaviour Prefab => _prefab;
    }
}