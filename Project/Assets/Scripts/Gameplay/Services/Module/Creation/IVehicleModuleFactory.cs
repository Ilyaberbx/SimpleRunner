using UnityEngine;

namespace Factura.Gameplay.Services.Module
{
    public interface IVehicleModuleFactory
    {
        VehicleModuleBehaviour Create(Vector3 at);
    }
}