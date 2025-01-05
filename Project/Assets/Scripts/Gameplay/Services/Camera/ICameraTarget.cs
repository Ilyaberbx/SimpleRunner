using UnityEngine;

namespace Factura.Gameplay.Services.Camera
{
    public interface ICameraTarget
    {
        Transform CameraFollow { get; }

        Transform CameraLookAt { get; }
    }
}