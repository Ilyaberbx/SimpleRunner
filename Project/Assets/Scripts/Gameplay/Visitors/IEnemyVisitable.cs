using System.Threading.Tasks;

namespace Factura.Gameplay.Visitors
{
    public interface IEnemyVisitable
    {
        Task Accept(IEnemyVisitor visitor);
    }
}