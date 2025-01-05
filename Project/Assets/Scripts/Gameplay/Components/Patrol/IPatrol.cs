using System.Threading;
using System.Threading.Tasks;

namespace Factura.Gameplay.Patrol
{
    public interface IPatrol
    {
        Task ConductAsync(CancellationToken token);

        public void Stop();
    }
}