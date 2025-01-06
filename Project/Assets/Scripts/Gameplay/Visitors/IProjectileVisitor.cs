using Factura.Gameplay.Projectiles;

namespace Factura.Gameplay.Visitors
{
    public interface IProjectileVisitor
    {
        void Visit(ProjectileBehaviour projectileBehaviour);
    }
}