using Factura.Gameplay.Enemies;

namespace Factura.Gameplay.Visitors
{
    public interface IProjectileVisitor
    {
        void Visit(EnemyBehaviour enemy);
    }
}