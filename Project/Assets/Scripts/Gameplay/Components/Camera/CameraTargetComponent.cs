using UnityEngine;

namespace Factura.Gameplay.Camera
{
    public sealed class CameraTargetComponent : ICameraTarget
    {
        public Transform CameraFollow { get; private set; }
        public Transform CameraLookAt { get; private set; }

        public void SetFollow(Transform point)
        {
            CameraFollow = point;
        }

        public void SetLookAt(Transform point)
        {
            CameraLookAt = point;
        }
    }
}