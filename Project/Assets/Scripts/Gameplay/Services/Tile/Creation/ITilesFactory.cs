using Factura.Gameplay.Tile;
using UnityEngine;

namespace Factura.Gameplay.Services.Tile.Creation
{
    public interface ITilesFactory
    {
        TileBehaviour Create(Vector3 at, Quaternion rotation, Transform parent = null);
    }
}