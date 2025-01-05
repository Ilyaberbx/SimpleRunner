using System.Threading.Tasks;
using Factura.Gameplay.Car;

namespace Factura.Gameplay.Visitors
{
    public interface IEnemyVisitor
    {
        Task Visit(CarBehaviour carBehaviour);
    }
}