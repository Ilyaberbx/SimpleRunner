using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Better.Attributes.Runtime.Select;
using Better.Commons.Runtime.Extensions;
using Better.Contexts.Runtime.Installers;
using UnityEngine;

namespace Factura.Core
{
    public class MonoContextAdapter : MonoBehaviour
    {
        [Select] [SerializeReference] private Installer[] _installers;

        private CancellationTokenSource _tokenSource;

        public Task EnterAsync()
        {
            _tokenSource = new CancellationTokenSource();
            return InstallServices(_tokenSource.Token);
        }

        public void Exit()
        {
            _tokenSource?.Cancel();

            UninstallServices();
        }

        private async Task InstallServices(CancellationToken cancellationToken)
        {
            await _installers.Select(x => x.InstallBindingsAsync(cancellationToken)).WhenAll();
        }

        private void UninstallServices()
        {
            foreach (var installer in _installers)
            {
                installer.UninstallBindings();
            }
        }
    }
}