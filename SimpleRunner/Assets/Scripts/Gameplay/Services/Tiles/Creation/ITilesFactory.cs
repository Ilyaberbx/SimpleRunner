using Gameplay.Tiles;
using UnityEngine;

namespace Gameplay.Services.Tiles.Creation
{
    public interface ITilesFactory
    {
        GroundTileBehaviour Create(Vector3 at, Quaternion rotation, Transform parent = null);
    }
}