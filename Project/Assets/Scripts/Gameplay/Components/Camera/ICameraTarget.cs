using UnityEngine;

namespace Factura.Gameplay.Camera
{
    public interface ICameraTargetReadonly
    {
        Transform CameraFollow { get; }

        Transform CameraLookAt { get; }
    }

    public interface ICameraTarget : ICameraTargetReadonly
    {
        public void SetFollow(Transform point);
        public void SetLookAt(Transform point);
    }
}