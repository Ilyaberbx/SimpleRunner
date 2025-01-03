using Cinemachine;
using Factura.Gameplay.Services.Camera;

namespace Factura.Gameplay.Extensions
{
    public static class CameraExtensions
    {
        public static void SetTarget(this ICinemachineCamera source, ICameraTarget target)
        {
            LookAt(source, target);
            source.Follow = target.CameraFollow;
        }

        public static void LookAt(this ICinemachineCamera source, ICameraTarget target)
        {
            source.LookAt = target.CameraLookAt;
        }
    }
}