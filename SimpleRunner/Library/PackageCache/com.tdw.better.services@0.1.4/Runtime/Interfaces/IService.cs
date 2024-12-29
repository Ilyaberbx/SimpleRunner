using System.Threading;
using System.Threading.Tasks;

namespace Better.Services.Runtime.Interfaces
{
    public interface IService
    {
        public bool Initialized { get; }

        public Task InitializeAsync(CancellationToken cancellationToken);

        public Task PostInitializeAsync(CancellationToken cancellationToken);
    }
}