using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using UnityEngine;

namespace Factura.Global.Services.AssetsManagement
{
    public class ResourcesProviderService : PocoService, IAssetsProvider
    {
        private const string ResourcesRootFolder = "Resources/";

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task<TAsset[]> LoadAll<TAsset>(string address) where TAsset : Object
        {
            var path = Path.Combine(ResourcesRootFolder, address);
            var assets = Resources.LoadAll<TAsset>(path);
            return Task.FromResult(assets);
        }

        public Task<TAsset> Load<TAsset>(string address) where TAsset : Object
        {
            var path = Path.Combine(ResourcesRootFolder, address);
            var asset = Resources.Load<TAsset>(path);
            return Task.FromResult(asset);
        }
    }
}