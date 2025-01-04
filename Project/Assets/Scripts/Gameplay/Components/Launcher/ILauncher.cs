using UnityEngine;

namespace Factura.Gameplay.Launcher
{
    public interface ILauncher
    {
        void Launch(float deltaTime, Vector3 mousePosition);
    }
}