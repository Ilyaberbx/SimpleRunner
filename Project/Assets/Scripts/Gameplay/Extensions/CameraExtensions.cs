using Cinemachine;
using Factura.Gameplay.Camera;

namespace Factura.Gameplay.Extensions
{
    public static class CameraExtensions
    {
        public static void SetTarget(this ICinemachineCamera source, ICameraTargetReadonly target)
        {
            LookAt(source, target);
            source.Follow = target.CameraFollow;
        }

        public static void LookAt(this ICinemachineCamera source, ICameraTargetReadonly target)
        {
            source.LookAt = target.CameraLookAt;
        }
    }
}