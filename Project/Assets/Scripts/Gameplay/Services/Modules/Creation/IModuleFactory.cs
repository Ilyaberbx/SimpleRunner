using Factura.Gameplay.Modules;

namespace Factura.Gameplay.Services.Modules
{
    public interface IModuleFactory
    {
        TModule Create<TModule>() where TModule : BaseModuleBehaviour;
    }
}