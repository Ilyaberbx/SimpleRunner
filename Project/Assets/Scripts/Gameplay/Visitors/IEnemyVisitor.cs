using Factura.Gameplay.Car;

namespace Factura.Gameplay.Visitors
{
    public interface IEnemyVisitor
    {
        void Visit(CarBehaviour carBehaviour);
    }
}