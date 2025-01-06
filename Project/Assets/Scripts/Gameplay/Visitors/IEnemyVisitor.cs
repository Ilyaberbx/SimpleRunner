using Factura.Gameplay.Enemy.Stickman;

namespace Factura.Gameplay.Visitors
{
    public interface IEnemyVisitor
    {
        void Visit(StickmanBehaviour stickmanBehaviour);
    }
}