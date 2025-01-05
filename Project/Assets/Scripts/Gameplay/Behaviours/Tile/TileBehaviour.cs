using UnityEngine;

namespace Factura.Gameplay.Tile
{
    [RequireComponent(typeof(MeshRenderer))]
    public sealed class TileBehaviour : MonoBehaviour
    {
        public MeshRenderer MeshRenderer { get; private set; }

        private void OnValidate()
        {
            if (MeshRenderer == null)
            {
                MeshRenderer = GetComponent<MeshRenderer>();
            }
        }
    }
}