using Factura.Gameplay.Target;

namespace Factura.Gameplay.Movement
{
    public interface IDynamicMovable : IMovable
    {
        void SetTarget(ITarget target);
    }
}