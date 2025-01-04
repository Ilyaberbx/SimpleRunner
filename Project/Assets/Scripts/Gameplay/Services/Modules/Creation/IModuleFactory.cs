using UnityEngine;

namespace Factura.Gameplay.Services.Modules
{
    public interface IModuleFactory
    {
        BaseModuleBehaviour Create(Vector3 at);
    }
}