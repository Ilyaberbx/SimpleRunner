using Factura.Gameplay.Tile;
using UnityEngine;

namespace Factura.Gameplay.Services.Tiles.Creation
{
    public interface ITilesFactory
    {
        GroundTileBehaviour Create(Vector3 at, Quaternion rotation, Transform parent = null);
    }
}