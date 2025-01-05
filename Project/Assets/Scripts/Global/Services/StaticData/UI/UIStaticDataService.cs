using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Better.Services.Runtime;
using Factura.Global.Services.AssetsManagement;
using Factura.UI.Services;

namespace Factura.Global.Services.StaticData
{
    public sealed class UIStaticDataService : PocoService, IUIStaticDataProvider
    {
        private ResourcesProviderService _assetsProvider;

        private PopupsConfiguration _popupsConfiguration;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override async Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _assetsProvider = ServiceLocator.Get<ResourcesProviderService>();
            _popupsConfiguration = await LoadPopupsConfiguration();
        }

        private Task<PopupsConfiguration> LoadPopupsConfiguration() =>
            _assetsProvider.Load<PopupsConfiguration>(StaticDataAddresses.Popups);


        public PopupData GetPopupConfiguration(PopupType type) =>
            _popupsConfiguration.Data.FirstOrDefault(temp => temp.Type == type);
    }
}