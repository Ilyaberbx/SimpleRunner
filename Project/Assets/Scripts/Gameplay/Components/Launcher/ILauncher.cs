using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Factura.Gameplay.Launcher
{
    public interface ILauncher
    {
        Task Launch(float deltaTime, Vector3 mousePosition, CancellationToken token);
    }
}