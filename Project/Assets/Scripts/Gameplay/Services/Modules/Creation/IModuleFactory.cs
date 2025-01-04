using Factura.Gameplay.Modules;
using UnityEngine;

namespace Factura.Gameplay.Services.Modules
{
    public interface IModuleFactory
    {
        TModule Create<TModule>(Vector3 at) where TModule : VehicleModuleBehaviour;
    }
}