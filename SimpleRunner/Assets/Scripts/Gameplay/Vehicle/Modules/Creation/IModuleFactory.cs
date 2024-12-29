namespace Gameplay.Vehicle.Modules
{
    public interface IModuleFactory
    {
        TModule Create<TModule>() where TModule : BaseModuleBehaviour;
    }
}