using System;
using Cinemachine;
using UnityEngine;

namespace Factura.Gameplay.Services.Camera
{
    [Serializable]
    public sealed class BrainCameraData
    {
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private CameraType _type;

        public CinemachineVirtualCamera Camera => _camera;
        public CameraType Type => _type;
    }

    public enum CameraType
    {
        PreStartCamera,
        FollowCamera,
    }
}