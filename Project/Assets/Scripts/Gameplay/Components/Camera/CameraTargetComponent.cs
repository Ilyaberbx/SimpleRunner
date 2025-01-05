using Factura.Gameplay.Services.Camera;
using UnityEngine;

namespace Factura.Gameplay.Camera
{
    public sealed class CameraTargetComponent : ICameraTarget
    {
        public Transform CameraFollow { get; }
        public Transform CameraLookAt { get; }

        public CameraTargetComponent(Transform cameraFollow, Transform cameraLookAt)
        {
            CameraFollow = cameraFollow;
            CameraLookAt = cameraLookAt;
        }
    }
}