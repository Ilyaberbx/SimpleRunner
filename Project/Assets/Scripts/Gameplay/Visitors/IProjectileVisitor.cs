using Factura.Gameplay.Enemy;

namespace Factura.Gameplay.Visitors
{
    public interface IProjectileVisitor
    {
        void Visit(EnemyBehaviour enemy);
    }
}