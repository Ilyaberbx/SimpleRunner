using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Factura.Global.Services.AssetsManagement
{
    [Serializable]
    public class ResourcesProviderService : PocoService, IAssetsProvider
    {
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
            var assets = Resources.LoadAll<TAsset>(address);
            return Task.FromResult(assets);
        }

        public Task<TAsset> Load<TAsset>(string address) where TAsset : Object
        {
            var asset = Resources.Load<TAsset>(address);
            return Task.FromResult(asset);
        }
    }
}