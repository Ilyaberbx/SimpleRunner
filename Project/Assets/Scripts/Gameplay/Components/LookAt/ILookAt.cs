using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Factura.Gameplay.LookAt
{
    public interface ILookAt
    {
        Task LookAt(Vector3 target, CancellationToken token);
    }
}