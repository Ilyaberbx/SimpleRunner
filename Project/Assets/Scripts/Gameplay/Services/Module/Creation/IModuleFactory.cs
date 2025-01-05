using UnityEngine;

namespace Factura.Gameplay.Services.Module
{
    public interface IModuleFactory
    {
        VehicleModuleBehaviour Create(Vector3 at);
    }
}