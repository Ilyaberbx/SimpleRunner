using System;
using System.Threading;
using System.Threading.Tasks;
using Factura.Gameplay.Target;

namespace Factura.Gameplay.Patrol
{
    public interface IPatrol
    {
        event Action<ITarget> OnTargetChanged;
        Task ConductAsync(CancellationToken token);
        public void Stop();
    }
}