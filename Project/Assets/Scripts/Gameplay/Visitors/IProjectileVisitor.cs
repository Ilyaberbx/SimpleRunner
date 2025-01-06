using Factura.Gameplay.Enemy.Stickman;

namespace Factura.Gameplay.Visitors
{
    public interface IProjectileVisitor
    {
        void Visit(StickmanBehaviour stickman);
    }
}