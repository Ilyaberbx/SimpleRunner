using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using Factura.Gameplay.Modules;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Factura.Gameplay.Services.Modules
{
    [Serializable]
    public sealed class ModuleService : PocoService<ModuleServiceSettings>
    {
        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public TModule Create<TModule>(Vector3 at = default) where TModule : BaseModuleBehaviour
        {
            return null;
        }

        public void Destroy<TModule>(TModule module) where TModule : BaseModuleBehaviour
        {
            var gameObject = module.gameObject;
            Object.Destroy(gameObject);
        }
    }
}