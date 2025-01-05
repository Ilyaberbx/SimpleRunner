using System.Threading;
using System.Threading.Tasks;
using Factura.Gameplay.Health;

namespace Factura.Gameplay.Attack
{
    public interface IAttack
    {
        public Task ProcessAsync(IHealth health, CancellationToken token);
    }
}