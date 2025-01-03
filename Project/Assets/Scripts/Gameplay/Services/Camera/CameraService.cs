using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using Cinemachine;
using Factura.Gameplay.Extensions;
using UnityEngine;

namespace Factura.Gameplay.Services.Camera
{
    [Serializable]
    public sealed class CameraService : PocoService
    {
        [SerializeField] private CinemachineBrain _brain;
        [SerializeField] private CameraData[] _camerasData;

        public UnityEngine.Camera MainCamera { get; private set; }

        private CameraType _currentType;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            MainCamera = _brain.GetComponent<UnityEngine.Camera>();

            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void SetActive(CameraType type)
        {
            if (!TryGetData(type, out var data))
            {
                return;
            }

            var cameraToActivate = data.Camera;
            foreach (var camera in _camerasData.Select(temp => temp.Camera))
            {
                camera.Priority = 0;
            }

            cameraToActivate.Priority = 1;
        }

        public void SetTarget(ICameraTarget target, CameraType type, bool follow)
        {
            if (!TryGetData(type, out var data))
            {
                return;
            }

            var cameraToSet = data.Camera;

            if (follow)
            {
                cameraToSet.SetTarget(target);
                return;
            }

            cameraToSet.LookAt(target);
        }

        private bool TryGetData(CameraType type, out CameraData data)
        {
            data = _camerasData.FirstOrDefault(temp => temp.Type == type);
            return data != null;
        }
    }
}