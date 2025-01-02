using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using UnityEngine;

namespace Factura.Global.Services
{
    [Serializable]
    public sealed class InputService : PocoService
    {
        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public bool IsMouse(int index)
        {
            return Input.GetMouseButton(index);
        }

        public Vector3 GetMousePosition()
        {
            return Input.mousePosition;
        }
    }
}