using Gameplay.Vehicle.Modules;

namespace Gameplay.Services.Modules
{
    public interface IModuleFactory
    {
        TModule Create<TModule>() where TModule : BaseModuleBehaviour;
    }
}